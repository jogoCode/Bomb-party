using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShower : MonoBehaviour
{
    [SerializeField] private float _rangeDistance;
    [SerializeField] private GameObject[] _BombPrefabs;
    [SerializeField] float _spawnCooldown;

    private void Start()
    {
        StartCoroutine(SpawnCooldown());
    }
    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(_spawnCooldown);
        Spawn();
    }
    public void Spawn()
    {
        int randomBomb = Random.Range(0, _BombPrefabs.Length);
        float randomPosX = Random.Range(-_rangeDistance, _rangeDistance);
        float randomPosZ = Random.Range(-_rangeDistance, _rangeDistance);
        Vector3 randomPos = new Vector3(randomPosX, transform.position.y, randomPosZ);
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        SoundManager.Instance.PlaySFX("BombQuiTombe");
        Instantiate(_BombPrefabs[randomBomb], randomPos, randomRotation);
        StartCoroutine(SpawnCooldown());
    }
}
