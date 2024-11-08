using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<EnemyData> enemyTypes; // List of EnemyType ScriptableObjects
    public Transform player; // Reference to the player
    public float spawnRadius = 10f; // Radius around the player to spawn enemies
    public float spawnInterval = 2f; // Interval between spawns
    public float minSpawnDistance = 2f; // Minimum distance from the player to spawn enemies

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }

    private void SpawnEnemy()
    {
        foreach (var enemyType in enemyTypes)
        {
            float randomValue = Random.Range(0f, 100f);
            if (randomValue < enemyType.spawnChance)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                Instantiate(enemyType.enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomCircle;
        Vector3 spawnPosition;

        do
        {
            randomCircle = Random.insideUnitCircle * spawnRadius;
            spawnPosition = new Vector3(player.position.x + randomCircle.x, player.position.y + randomCircle.y);
        } while (Vector3.Distance(spawnPosition, player.position) < minSpawnDistance);

        return spawnPosition;
    }
}