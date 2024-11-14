using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bombe : MonoBehaviour
{
    [SerializeField] ScriptableBomb _bombe;
    [SerializeField] ScriptableBomb _bombeInstance;
    [SerializeField] Rigidbody _rb;
    
    [SerializeField] GameObject explosionPrefab;

    private void Start()
    {
        _bombeInstance = Instantiate(_bombe);
        _rb = GetComponent<Rigidbody>();
        


    }

    private void OnCollisionEnter(Collision collision)
    {
     
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.GetComponent<PlayerController>() == null) 
        {
            StartTimer();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision != null && collision.gameObject.GetComponent<PlayerController>())
        {
            if (_bombe._timerExplosion <= 0)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    void StartTimer()
    {

        _bombe._timerExplosion -= Time.deltaTime;
    }

}
