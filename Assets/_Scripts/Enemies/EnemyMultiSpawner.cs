using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultiSpawner : MonoBehaviour
{
    [Header("Debug")]
    public float currentSpawnedEnemies;
    [SerializeField] private float totalSpawnedCount;

    [Header("Configuration")]
    [SerializeField] float spawnDelay;
    [SerializeField] float maxEnemySpawn;
    [SerializeField] float maxMaxEnemySpawn;

    [Header("References")]
    [SerializeField] List<Transform> spawnPositions;
    private int currentSpawnIndex = 0;

    [Header("Prefabs")]
    [SerializeField] GameObject enemyPrefab;

    [Header("References")]
    [SerializeField] PlayerHUD playerHUD;

    private void Start()
    {
        currentSpawnedEnemies = 0;
        StartCoroutine(nameof(InstaciateEnemy));
    }

    IEnumerator InstaciateEnemy()
    {
        Instantiate(enemyPrefab, spawnPositions[currentSpawnIndex].position, Quaternion.identity);
        currentSpawnIndex++; if (currentSpawnIndex > spawnPositions.Count -1) currentSpawnIndex = 0;

        totalSpawnedCount++;
        currentSpawnedEnemies++;
        if (currentSpawnedEnemies < maxEnemySpawn && currentSpawnedEnemies < maxMaxEnemySpawn)
        {
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(nameof(InstaciateEnemy));            
        }
        CheckForSpawnUpgrade();
    }

    private void CheckForSpawnUpgrade()
    {
        int threshold = Mathf.CeilToInt(maxEnemySpawn * 1.25f);

        if (totalSpawnedCount > threshold)
        {
            maxEnemySpawn = Mathf.CeilToInt(maxEnemySpawn * 1.25f);
            totalSpawnedCount = 0;
        }
    }

    public void EnemyKilled()
    {
        playerHUD.UpdateEnemyKills();
        currentSpawnedEnemies--;
        if (currentSpawnedEnemies < maxEnemySpawn)
        {            
            StartCoroutine(nameof(InstaciateEnemy));
        }
    }
}
