using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ff");
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("ntm");
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            other.gameObject.SetActive(false);

            ScoreManager score = GameManager.Instance.GetScoreManager();

            score.AddPlayerToList(player, score.Bonus);
        }
    }

}
