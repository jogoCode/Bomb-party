using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombe : MonoBehaviour
{
    [SerializeField] ScriptableBomb _bombe;
    [SerializeField] ScriptableBomb _bombeInstance;
    [SerializeField] Rigidbody _rb;

    private void Start()
    {
        _bombeInstance = Instantiate(_bombe);
        _rb = GetComponent<Rigidbody>();


    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision == gameObject.GetComponent<PlayerController>())
        //{
        //    // explosion + player = mort // anim de l'explosion 
        //    Destroy(collision.gameObject);
        //}
    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (collision != null) // && collision != gameObject.GetComponent<PlayerController>())
        {
            if (_bombe._timerExplosion <= 0)
            {
                // explose  dans le radius check les players qui sont dedans
                  // anim de l'explosion 
                //Destroy(gameObject); 
            }
        }
    }

    void StartTimer()
    {

        _bombe._timerExplosion -= Time.deltaTime;
    }

}
