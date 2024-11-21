using System;
using Random = UnityEngine.Random;
using UnityEngine;
using TMPro;


public class ParryBomb : MonoBehaviour
{
    public const float MAX_VEL_MAGNITUDE = 70;
    public const float MIN_VEL_MAGNITUDE = 15;

    ParryBombManager m_parryBombManager;
    [SerializeField] Rigidbody m_rb;
    GameManager m_gm;


    PlayerController m_owner = null;
    [SerializeField] float _explosionTime = 60;
    [SerializeField] float _bombTimer = 0;
    [SerializeField] bool _boom = false;
    [SerializeField] float m_velMultiplier = 2f;


    TrailRenderer m_trailRenderer;
   




    public event Action OnPlayerTouched;
    public event Action<float> OnParried;
    public event Action<PlayerController> OnExplode;


    TMP_Text m_bombTimerDisplay;
    Oscillator m_oscillator;

    public PlayerController Owner
    {
        get { return m_owner; }
    }

    void Start()
    {
        ResetBombTimer();
        m_rb = GetComponent<Rigidbody>();
        m_trailRenderer = GetComponent<TrailRenderer>();
        m_bombTimerDisplay = GetComponentInChildren<TMP_Text>();
        m_oscillator = GetComponent<Oscillator>();  
        OnParried += m_oscillator.StartOscillator;
        m_gm = GameManager.Instance;
       
    }

    void Update()
    {
        BombTimer();
    }

    public void StartParryBomb()
    {
        Vector3 randVect = new Vector3(Random.Range(-1f, 1f),0,Random.Range(-1f, 1f)).normalized;
      
        m_rb.AddForce(randVect * 15, ForceMode.Impulse); //TODO replace this hard value
        Debug.Log(m_rb.ToString());
    }


    private void FixedUpdate()
    {
        if (m_rb.velocity.magnitude < MIN_VEL_MAGNITUDE)
        {
            m_rb.velocity = m_rb.velocity.normalized * MIN_VEL_MAGNITUDE;
        }
    }


    void ResetBombTimer()
    {
        _bombTimer = _explosionTime;
    }


    void BombTimer()
    {
        if (_bombTimer > 0)
        {
            _bombTimer = Mathf.Clamp(_bombTimer, 0, _explosionTime) - Time.deltaTime; // diminue le timer de la bombe au fine du temps 
            m_bombTimerDisplay.text = Math.Ceiling(_bombTimer).ToString();

        }   
        else
        {
            _boom = true;
            SoundManager.Instance.PlaySFX("Explosion");
            gameObject.SetActive(false);
            FeedBackManager.Instance.InstantiateParticle(FeedBackManager.Instance.m_explosionVfx, transform.position, transform.rotation);
            OnExplode?.Invoke(m_owner);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.Instance.PlaySFX("PushSurBomb");
       
        m_rb.constraints = RigidbodyConstraints.FreezePositionY;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if(player == null) return;
        // Check if the owner 
        if(player.name.Contains("Player") && player != m_owner)  //Player was touched
        {
            SoundManager.Instance.PlaySFX("Explosion");
            FeedBackManager.Instance.InstantiateParticle(FeedBackManager.Instance.m_explosionVfx,player.transform.position,player.transform.rotation);
            ScoreManager sm = m_gm.GetScoreManager();
            sm.AddPlayerToList(player,sm.Bonus);
            other.gameObject.SetActive(false);
            ResetBombTimer();
            m_rb.velocity = Vector3.zero; //TODO METTRE DANS UN FONCTION RESET
            m_owner = null; //TODO METTRE DANS UN FONCTION RESET
            OnPlayerTouched?.Invoke();
        }
    }


    public void Parry(Vector3 direction,PlayerController player)
    {
        FeedBackManager fbm = FeedBackManager.Instance;
        if (m_owner != null)
        {
            m_owner.SetLayer(GameManager.PLAYER_LAYER);
        }
        SetOwner(player);
        player.SetLayer(GameManager.PLAYER_PARRY_BOMB_LAYER);
        Vector3 oldVel = m_rb.velocity;
        m_rb.velocity = Vector3.zero;
        SetTrailRendererMat(player);
        if (oldVel != Vector3.zero)
        {
            m_rb.AddForce(direction * (oldVel.magnitude*m_velMultiplier), ForceMode.Impulse); //TODO replace this hard value
        }
        else
        {
            m_rb.AddForce(direction*25, ForceMode.Impulse); //TODO replace this hard value
        }
        OnParried?.Invoke(-5);
        fbm.InstantiateParticle(fbm.m_impactVfx, transform.position, transform.rotation);
        return;

    }

    public void SetOwner(PlayerController owner)
    {
        m_owner = owner;
    }


    public void SetTrailRendererMat(PlayerController player)
    {

        switch (player.PlayerId)
        {
            case 0:
                m_trailRenderer.material = GameManager.Instance.PLAYER1;
                break;
            case 1:
                m_trailRenderer.material = GameManager.Instance.PLAYER2;
                break;
            case 2:
                m_trailRenderer.material = GameManager.Instance.PLAYER3;
                break;
            case 3:
                m_trailRenderer.material = GameManager.Instance.PLAYER4;
                break;
        }
    }
}
