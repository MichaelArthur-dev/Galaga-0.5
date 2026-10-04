using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject gloopPrefab; // Prefab for the enemy
    public Transform[] formationPoints; // Points where enemies will be spawned
    public float spawnHeight = 2f; // Height at which enemies will be spawned

    void Start()
    {
        foreach (Transform point in formationPoints)
        {
            Vector3 spawnPosition = point.position + Vector3.up * spawnHeight;
            GameObject enemy = Instantiate(gloopPrefab, spawnPosition, Quaternion.identity);

            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            movement.formationPoint = point;
        }
    }
}
