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
    [SerializeField] float m_zoomInFOV = 20f;
    [SerializeField] float m_defaultFOV = 60f;

    [SerializeField] float m_zoomDistance = 5f;
    Vector3 m_zoomVector;
    bool m_isZooming;
    Camera m_camera;

    Vector3 m_initialPosition;
    Quaternion m_initialRotation;



    private void Start()
    {
        m_initialPosition = transform.position;
        m_camera = GetComponent<Camera>();
    }

    private void Update()
    {
        // Appuie sur "Z" pour activer/désactiver le zoom
        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_isZooming = true;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            m_isZooming = false;
        }

        if (m_isZooming)
        {
            // Zoom vers la cible : réduit le FOV et rapproche la caméra
            m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, m_zoomInFOV, Time.deltaTime * m_zoomSpeed);

            // Positionne la caméra à la distance spécifiée par rapport à la cible
            Vector3 targetPosition = m_target.transform.position - m_camera.transform.forward * m_zoomDistance;
            m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, targetPosition, Time.deltaTime * m_zoomSpeed);

            // Oriente la caméra vers la cible
            Quaternion targetRotation = Quaternion.LookRotation(m_target.transform.position - m_camera.transform.position);
            m_camera.transform.rotation = Quaternion.Slerp(m_camera.transform.rotation, targetRotation, Time.deltaTime * m_zoomSpeed);
        }
        else
        {
            // Retourne au champ de vision par défaut et à la position initiale
            m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, m_defaultFOV, Time.deltaTime * m_zoomSpeed);
            m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, m_initialPosition, Time.deltaTime * m_zoomSpeed);
            m_camera.transform.rotation = Quaternion.Slerp(m_camera.transform.rotation, m_initialRotation, Time.deltaTime * m_zoomSpeed);
        }

    }





}
