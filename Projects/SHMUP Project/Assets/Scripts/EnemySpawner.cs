using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance { get; private set; }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2.0f;
    [SerializeField] private float spawnRangeX = 8.0f;
    public static List<GameObject> spawnedEnemyShips = new List<GameObject>();
    [SerializeField] public GameObject bossShip;
    private bool isBossSpawned = false;
    private int bossSpawnValue = 1500;

    private float spawnTimer;
    private float spawnY;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initiate y-coordinate of spawner
        spawnY = ScreenDetector.ScreenTop;
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

        // Spawn boss if score reaches the multiples of bossSpawnValue
        if (UI.instance.ScoreTracker >= bossSpawnValue && !isBossSpawned)
        {
            SpawnBoss();
        }
        // Destroy boss ship if it loses all health
        else if (bossShip.GetComponent<BossShip>().Health <= 0)
        {
            // Reset ship's position and health
            bossShip.transform.position = new Vector3(0, ScreenDetector.ScreenTop + 1.1f, 0);
            bossShip.GetComponent<BossShip>().Health = 50;
            isBossSpawned = false;
            bossShip.SetActive(false);

            UI.instance.AddScore(300);

            // Restore the ability token
            CollisionManager.instance.AbilityToken = true;
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

    // Spawn boss ship
    public void SpawnBoss()
    {
        isBossSpawned = true;
        if (!bossShip.activeInHierarchy)
        {
            bossShip.SetActive(true);
            UI.instance.ScoreTracker = 0;
        }
    }

    // Clear all spawned enemy ships and convert to score
    public void ExplodeShips()
    {
        foreach (GameObject ship in spawnedEnemyShips)
        {
            if (ship != null)
            {
                UI.instance.AddScore(10);
                Destroy(ship);
            }
        }
    }
}
