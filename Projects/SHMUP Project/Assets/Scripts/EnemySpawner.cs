using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2.0f;
    [SerializeField] private float spawnRangeX = 8.0f;
    public static List<GameObject> spawnedEnemyShips = new List<GameObject>();

    private float spawnTimer;
    private float spawnY;

    // Start is called before the first frame update
    void Start()
    {
        // Initiate y-coordinate of spawner
        spawnY = MovementController.screenTop;
    }

    void Update()
    {
        // Call SpawnEnemy() every spawnInterval (2s by default)
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; // Reset timer
        }
    }

    // Spawn enemy ship and keep track of them
    public void SpawnEnemy()
    {
        float spawnX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        GameObject ship = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedEnemyShips.Add(ship);
    }
}
