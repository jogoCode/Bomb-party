using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
    public static FeedBackManager Instance;

    public ParticleSystem m_explosionVfx;
    public ParticleSystem m_impactVfx;

    Coroutine m_FreezeFrameCoroutine;

    void Awake()
    {
        Debug.Log(Time.timeScale);
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


    public void FreezeFrame(float duration, float timeScale)
    {
        m_FreezeFrameCoroutine = StartCoroutine(FreezeFrameCoroutine(duration,timeScale));
    }


    public void CameraZoom()
    {

    }

    IEnumerator FreezeFrameCoroutine(float duration, float timeScale)
    {
        Time.timeScale = timeScale;
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1;
    }
}
