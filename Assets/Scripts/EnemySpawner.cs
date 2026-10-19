using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject gloopPrefab; // Gloop enemy prefab
    public GameObject strikerPrefab; // Striker enemy prefab
    public Transform[] formationPoints; // Positions in the formation
    public float spawnHeight = 2f; // Spawn distance above each point
    private List<GameObject> activeEnemies = new List<GameObject>(); // Tracks enemies still alive
    private int waveNumber = 40; // Current wave number

    void Start()
    {
        SpawnWave(); // Start the first wave
    }

    void Update()
    {
        if (PausedMenu.InputBlocked) return; // Stop wave spawning while paused
        activeEnemies.RemoveAll(enemy => enemy == null); // Remove destroyed enemies from the list
        if (activeEnemies.Count == 0)
        {
            SpawnWave(); // Start the next wave when all enemies are gone
        }
    }

    void SpawnWave()
    {
        waveNumber++; // Increase the wave number
        GameManager.instance.SetWave(waveNumber); // Update the wave display
        int difficultyStep = 0; // Keep the original difficulty before wave 10
        if (waveNumber >= 10)
        {
            difficultyStep = Mathf.Min(1 + (waveNumber - 10) / 5, 4); // Increase every five waves, capped at four steps
        }
        int gloopCount; // Number of Gloops to spawn
        int strikerCount; // Number of Strikers to spawn
        switch (waveNumber) // Choose the enemy mix for this wave
        {
            case 1:
                gloopCount = 4;
                strikerCount = 0;
                break;
            case 2:
                gloopCount = 6;
                strikerCount = 0;
                break;
            case 3:
                gloopCount = 4;
                strikerCount = 2;
                break;
            case 4:
                gloopCount = 6;
                strikerCount = 2;
                break;
            case 5:
                gloopCount = 6;
                strikerCount = 3;
                break;
            case 6:
                gloopCount = 7;
                strikerCount = 3;
                break;
            case 7:
                gloopCount = 7;
                strikerCount = 4;
                break;
            default:
                strikerCount = 4;
                if (waveNumber >= 10)
                {
                int patternPosition = (waveNumber - 10) % 3; // Repeat the pattern every three waves
                if (patternPosition == 1)
                {
                    strikerCount = 2;
                }
                else if (patternPosition == 2)
                {
                    strikerCount = 3;
                }
                }
                gloopCount = 12 - strikerCount; // Keep later waves at 12 enemies total
                break;
        }
        int enemyCount = gloopCount + strikerCount; // Total enemies for this wave
        for (int i = 0; i < enemyCount; i++) // Spawn one enemy at each formation point
        {
            Transform point = formationPoints[i];
            GameObject prefabToSpawn = gloopPrefab;
            if (i >= gloopCount)
            {
                prefabToSpawn = strikerPrefab; // Use Strikers after spawning the Gloops
            }
            Vector3 spawnPosition = point.position + Vector3.up * spawnHeight; // Spawn above the formation point
            GameObject enemy = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity); // Create the enemy
            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            movement.formationPoint = point; // Assign the enemy its formation point
            movement.moveSpeed *= 1f + difficultyStep * 0.1f; // Increase original speed by up to 40%
            EnemyShooting shooting = enemy.GetComponent<EnemyShooting>();
            if (shooting != null)
            {
                shooting.shootInterval *= 1f - difficultyStep * 0.1f; // Reduce original time between shots by up to 40%
            }
            activeEnemies.Add(enemy); // Track the enemy until it is destroyed
        }
    }
}