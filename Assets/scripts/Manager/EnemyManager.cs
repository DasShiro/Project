using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyManager : MonoBehaviour
{
    private List<Enemy> activeEnemies = new List<Enemy>(); // List of active enemies
    public ScoreManager scoreManager; // Reference to the ScoreManager
    public float minSpawnDistance = 8f; // Minimum distance from the player

    public List<EnemyData> enemyTypes; // List of EnemyType ScriptableObjects
    public Transform player; // Reference to the player
    public float spawnRadius = 10f; // Radius around the player where enemies will spawn
    public float spawnInterval = 1f; // Time interval between enemy spawns
    public int maxActiveEnemies = 100; // Maximum number of active enemies allowed
    public int increasePerWave = 5; // Number of enemies to add per wave

    public static EnemyManager Instance;

    private Coroutine spawnCoroutine; // Reference to the spawning coroutine
    private TilemapCollider2D tilemapCollider; // Reference to the TilemapCollider2D

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        // Check if the player is already set
        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.transform;
        }

        if (player == null)
        {
            return; // Prevent spawning enemies if player is not available
        }

        // Get the TilemapCollider2D component
        tilemapCollider = Object.FindFirstObjectByType<TilemapCollider2D>();
        if (tilemapCollider == null)
        {
            Debug.LogError("TilemapCollider2D not found in the scene.");
        }
    }

    // Method to start spawning enemies for the duration of the wave
    public void StartSpawningEnemies(int waveNumber)
    {
        // Increase the maximum number of active enemies for the new wave
        maxActiveEnemies += increasePerWave;

        // Stop any existing spawning coroutine
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnEnemies(waveNumber));
    }

    public IEnumerator SpawnEnemies(int waveNumber)
    {
        while (WaveManager.instance.IsWaveRunning())
        {
            if (activeEnemies.Count < maxActiveEnemies) // Check if we can spawn more enemies
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval before spawning the next enemy
        }
    }

    public void SpawnEnemy()
    {
        foreach (var enemyType in enemyTypes)
        {
            float randomValue = Random.Range(0f, 100f);
            if (randomValue < enemyType.spawnChance)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                Enemy enemy = Instantiate(enemyType.enemyPrefab, spawnPosition, Quaternion.identity).GetComponent<Enemy>();
                enemy.SetPlayerReference(player); // Set the player reference for the enemy
                activeEnemies.Add(enemy); // Add the enemy to the active list
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomCircle;
        Vector3 spawnPosition;
        int attempts = 0; // Zähler für Versuche
        const int maxAttempts = 100; // Maximale Anzahl der Versuche

        do
        {
            randomCircle = Random.insideUnitCircle * spawnRadius; // Erzeuge einen zufälligen Punkt innerhalb eines Kreises
            spawnPosition = new Vector3(player.position.x + randomCircle.x, player.position.y + randomCircle.y);
            attempts++;

            // Überprüfen, ob die maximale Anzahl der Versuche erreicht wurde
            if (attempts >= maxAttempts)
            {
                Debug.LogWarning("Maximale Anzahl der Versuche erreicht für Spawn-Position. Rückgabe der Standardposition.");
                return player.position + new Vector3(minSpawnDistance, 0, 0); // Rückgabe einer Standardposition, die minSpawnDistance entfernt ist
            }

        } while (Vector3.Distance(spawnPosition, player.position) < minSpawnDistance || !IsPositionInsideTilemap(spawnPosition));

        Debug.Log($"Spawn Position: {spawnPosition}"); // Debug-Log zur Überprüfung der Spawn-Position
        return spawnPosition;
    }

    private bool IsPositionInsideTilemap(Vector3 position)
    {
        // Check if the position is within the TilemapCollider2D
        return tilemapCollider != null && tilemapCollider.OverlapPoint(position);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
    }

    public void DestroyAllEnemies()
    {
        // Destroy all active enemies
        foreach (Enemy enemy in activeEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject); // Destroy the enemy's GameObject
            }
        }
        activeEnemies.Clear(); // Clear the active enemies list
    }
}