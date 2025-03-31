using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This code is created follow the tutorial from https://www.bilibili.com/video/BV1a84y1j7yC?vd_source=1acaaabcc1a8eca1cda53dd9f334fc00
public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; // Enemy spawn points

    public Wave[] waves; // Wave array
    private Wave currentWave; // Current wave
    private int currentWaveIndex = 0; // Current wave index

    private int enemyRemaining = 0; // Check enemy number

    private void Start()
    {
        // Check if spawn points available
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("Can't find enemy spawn points, please check it!");
        }

        StartCoroutine(NextWaveCoroutine());
    }
    // Create a coroutine
    private IEnumerator NextWaveCoroutine()
    {
        currentWaveIndex++;
        if (currentWaveIndex - 1 < waves.Length)
        {
            currentWave = waves[currentWaveIndex - 1];
            enemyRemaining = currentWave.count;
            // Random create enemy
            for (int i = 0; i < currentWave.count; i++)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Enemy enemy = Instantiate(currentWave.Enemy, spawnPoints[spawnIndex].position, Quaternion.identity);
                enemy.OnDeath += OnEnemyDeath; // On death event
                yield return new WaitForSeconds(currentWave.timeBetweenSpawn);
            }
        }
    }

    private void OnEnemyDeath()
    {
        // Start next wave
        enemyRemaining--;
        if (enemyRemaining == 0)
        {
            StartCoroutine(NextWaveCoroutine());
        }
    }
}