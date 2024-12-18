using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryBomb : MonoBehaviour
{
    
    PlayerController m_playerController;

    [SerializeField]BoxCollider m_boxCollider;



    void Start()
    {
        m_playerController = GetComponentInParent<PlayerController>();
        m_boxCollider = GetComponent<BoxCollider>();   
    }






    private void OnTriggerEnter(Collider other)
    {
        FeedBackManager fbm = FeedBackManager.Instance;
        Vector3 inputDir = new Vector3(m_playerController.GetLastInputDir().x, 0, m_playerController.GetLastInputDir().y);
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController)
        {
            FeedBackHitplayer(playerController);
        }
        if (other.GetComponent<VolleyBomb>() != null)
        {

            SoundManager.Instance.PlaySFX("PushSurBomb");
            //1e-07
            other.GetComponent<Rigidbody>().AddForce(new Vector3(m_playerController.GetLastInputDir().x,1, m_playerController.GetLastInputDir().y).normalized*50,ForceMode.Impulse);
            FeedBackManager.Instance.InstantiateParticle(FeedBackManager.Instance.m_impactVfx,other.transform.position,other.transform.rotation);
            return;
        }
      
        ParryBomb bomb = other.GetComponent<ParryBomb>();
        if (bomb == null) return;
        SoundManager.Instance.PlaySFX("WooshBatBomb");
        bomb.Parry(inputDir,m_playerController);
        fbm.FreezeFrame(0.06f,0.5f);     
    }


    private void FeedBackHitplayer(PlayerController otherPlayer)
    {
        if (!m_playerController.GetPlayerVisual().BatModel)
        {
            SoundManager.Instance.PlaySFX("PushSurPlayer");
        }
        else
        {
            SoundManager.Instance.PlaySFX("BatSurPlayer");
        }
        otherPlayer.ApplyImpulse(new Vector3(m_playerController.GetLastInputDir().x, 0, m_playerController.GetLastInputDir().y), 50); // TODO replace hard value
        otherPlayer.Hit();
        Vector3 particlePos = new Vector3(otherPlayer.transform.position.x, otherPlayer.transform.position.y+0.5f, otherPlayer.transform.position.z);
        FeedBackManager.Instance.InstantiateParticle(FeedBackManager.Instance.m_impactVfx, particlePos , otherPlayer.transform.rotation);
        otherPlayer.StartOscillator(5); // TODO  replace hard value
    }


}
