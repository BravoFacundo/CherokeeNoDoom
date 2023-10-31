using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float spawnDelay;

    [Header("References")]
    [SerializeField] private Transform spawnPos;

    [Header("Prefabs")]
    [SerializeField] private GameObject enemyPrefab;

    private void Start()
    {
        StartCoroutine(nameof(InstaciateEnemy));
    }

    IEnumerator InstaciateEnemy()
    {
        Instantiate(enemyPrefab, spawnPos.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(nameof(InstaciateEnemy));
    }
}
