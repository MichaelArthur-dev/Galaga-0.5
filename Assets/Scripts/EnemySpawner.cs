using UnityEngine;
using System.Collections.Generic;
public class EnemySpawner : MonoBehaviour
{
    public GameObject gloopPrefab; // Prefab for the enemy
    public GameObject strikerPrefab; // Prefab for the striker enemy
    public Transform[] formationPoints; // Points where enemies will be spawned
    public float spawnHeight = 2f; // Height at which enemies will be spawned
    private List<GameObject> activeEnemies = new List<GameObject>(); // List to keep track of active enemies
    private int waveNumber = 0; // Current wave number

    void Start()
    {
        SpawnWave(); // Creates the first wave when the scene starts
    }

    void Update()
    {
        if (PausedMenu.InputBlocked) return;
        activeEnemies.RemoveAll(enemy => enemy == null); // Clean up destroyed enemies
        if (activeEnemies.Count == 0)
        {
            SpawnWave(); // Starts the next wave when all current enemies are gone
        }
    }

    void SpawnWave()
    {
        waveNumber++;
        GameManager.instance.SetWave(waveNumber); // Updates the wave number shown by the game manager
        int gloopCount;
        int strikerCount;
        // Sets the enemy mix for each wave
        switch (waveNumber)
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
                    int patternPosition = (waveNumber - 10) % 3; // Repeats the striker count pattern every three waves
                    if (patternPosition == 1)
                    {
                        strikerCount = 2;
                    }
                    else if (patternPosition == 2)
                    {
                        strikerCount = 3;
                    }
                }
                gloopCount = 12 - strikerCount; // Keeps later waves at 12 enemies total
                break;
                }
            int enemyCount = gloopCount + strikerCount;
            for (int i = 0; i < gloopCount + strikerCount; i++) // Uses one formation point for each enemy
            {
            Transform point = formationPoints[i];
            GameObject prefabToSpawn = gloopPrefab;
            if (i >= gloopCount) // Uses striker prefabs for the points after the Gloops
            {
                prefabToSpawn = strikerPrefab;
            }
            Vector3 spawnPosition = point.position + Vector3.up * spawnHeight; // Spawns each enemy slightly above its formation point
            GameObject enemy = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity); 

            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            movement.formationPoint = point; // Gives the enemy its place in the formation to move back to
            activeEnemies.Add(enemy); // Tracks the enemy so the next wave waits until it is destroyed
        }
    }
}
