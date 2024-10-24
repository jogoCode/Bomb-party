using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Oscillator : MonoBehaviour
{

    [SerializeField] GameObject m_target;
    Vector3 m_baseTargetScale;

    [SerializeField] float m_spring;
    [SerializeField] float m_damp;
    float m_displacement;
    float m_velocity;

    public event Action<float> OnStartOscillator;


    void Start()
    {
        m_baseTargetScale = m_target.transform.localScale;
        OnStartOscillator += SetVelocity;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            SetVelocity(10);
        }

        float force = -m_spring * m_displacement - m_damp * m_velocity;
        m_velocity += force * Time.deltaTime;
        m_displacement += m_velocity * Time.deltaTime;
        Vector3 newScale = m_baseTargetScale + new Vector3(m_displacement, -m_displacement, m_displacement) * 2;
        newScale.x = Mathf.Clamp(newScale.x, 0,1000);
        newScale.y = Mathf.Clamp(newScale.y, 0, 1000);
        newScale.z = Mathf.Clamp(newScale.z, 0, 1000);
        m_target.transform.localScale = newScale;
    }

    void SetVelocity(float value)
    {
        m_velocity = value;
    }


    public void StartOscillator(float velocity)
    {
        OnStartOscillator?.Invoke(velocity);
    }

}
