using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] GameObject m_target;
    [SerializeField] Vector3 m_offset;
    [SerializeField] float m_zoomSpeed;
    Vector3 m_zoomVector;

    Vector3 m_originPosition;



    private void Start()
    {
        m_originPosition = transform.position;
    }

    private void Update()
    {

    }





}
