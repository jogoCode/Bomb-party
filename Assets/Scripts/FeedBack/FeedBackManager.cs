using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
    public static FeedBackManager Instance;

    public ParticleSystem m_explosionVfx;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Plus d'une instance feedback manager dans la scene");
            Destroy(gameObject);
            return;
        }
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void InstantiateParticle(ParticleSystem particle,Vector3 position, Quaternion rotation)
    {
        Instantiate(particle.gameObject,position,rotation);
    }
}
