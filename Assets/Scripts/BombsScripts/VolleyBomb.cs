using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class VolleyBomb : MonoBehaviour
{
    [SerializeField] private float _bombTimer = 10;
    [SerializeField] private bool _boom = false;
    [SerializeField] private bool _goundCollision = false;
    [SerializeField] private int _playerId = 0;
    [SerializeField] private bool _P1Lost = false;
    [SerializeField] private bool _P2Lost = false;
    [SerializeField] private bool _P3Lost = false;
    [SerializeField] private bool _P4Lost = false;
    private Vector3 _bombeCenter;
    private GameObject _newContact;
    private GameObject _currentContact;
    
    
    public GameObject test;

    void Update()
    {
        _bombeCenter = transform.position;
        BombTimer();
        if(_goundCollision == true || _boom == true)
        {
            Lost();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag == "Ground")
        {
            _goundCollision = true;
            //Debug.Log("Sol Touché !");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        _newContact = other.gameObject;
        _newContact.transform.position = other.gameObject.transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        _currentContact = other.gameObject;
        _currentContact.transform.position = other.gameObject.transform.position;
        
        Debug.Log(_newContact.GetComponent<Collider>().ClosestPoint(_bombeCenter));
        test.transform.position = _newContact.GetComponent<Collider>().ClosestPoint(_bombeCenter);
        
        if (_newContact.GetComponent<Collider>().ClosestPoint(_bombeCenter) == _bombeCenter)
        {
            if(_newContact.tag == "VolleyBombZoneP1") { _playerId = 1; }
            if(_newContact.tag == "VolleyBombZoneP2") { _playerId = 2; }
            if(_newContact.tag == "VolleyBombZoneP3") { _playerId = 3; }
            if(_newContact.tag == "VolleyBombZoneP4") { _playerId = 4; }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        _newContact = _currentContact;
    }
    void Lost()
    {
        if (_playerId == 1)
        {
            _P1Lost = true;
            //Debug.Log("Player 1 Lost");
        }
        else if (_playerId == 2)
        {
            _P2Lost = true;
            //Debug.Log("Player 2 Lost");
        }
        else if (_playerId == 3)
        {
            _P3Lost = true;
            //Debug.Log("Player 3 Lost");
        }
        else if (_playerId == 4)
        {
            _P4Lost = true;
            //Debug.Log("Player 4 Lost");
        }
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
}
