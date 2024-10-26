using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryBomb : MonoBehaviour
{

    ParryBombManager m_parryBombManager;

    public const float MAX_VEL_MAGNITUDE = 70;
    public float _bombTimer = 10;
    public bool _boom = false;
    Rigidbody m_rb;

    TrailRenderer m_trailRenderer;
    PlayerController m_owner = null;

    public event Action OnPlayerTouched;
    public event Action<float> OnParried;

    Oscillator m_oscillator;

    public PlayerController Owner
    {
        get { return m_owner; }
    }

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_trailRenderer = GetComponent<TrailRenderer>();
        m_oscillator = GetComponent<Oscillator>();  
        OnParried += m_oscillator.StartOscillator;
       
    }

    void Update()
    {
        BombTimer();
    }


    private void FixedUpdate()
    {

     

        //if (m_rb.velocity.magnitude > MAX_VEL_MAGNITUDE)
        //{
        //    // Réduire la vitesse à la limite maximale en conservant la direction
        //    return;
        //    m_rb.velocity = m_rb.velocity.normalized * MAX_VEL_MAGNITUDE;
        //}
    }
    void BombTimer()
    {
        if (_bombTimer > 0)
        {
            _bombTimer = Mathf.Clamp(_bombTimer, 0, 10) - Time.deltaTime; // diminue le titer de la bombe au fine du temps 
        }
        else
        {
            _boom = true;
            //Debug.Log("Timer of Bombe is = to : " + _bombTimer);
            //Debug.Log("Boom !");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log(other.gameObject.name);
 
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if(player == null) return;
        // Check if the owner
        if(m_owner !=null && player.name.Contains("Player") && player != m_owner)
        {
            FeedBackManager.Instance.InstantiateParticle(FeedBackManager.Instance.m_explosionVfx,player.transform.position,player.transform.rotation);
            other.gameObject.SetActive(false);
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
            m_rb.AddForce(direction * (oldVel.magnitude*1.001f), ForceMode.Impulse); //TODO replace this hard value
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
