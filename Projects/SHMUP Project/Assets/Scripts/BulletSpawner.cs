using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner instance { get; private set; }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireInterval = 1.0f;
    private float fireTimer;
    private Vector3 bulletSpawnPoint;
    public static List<GameObject> spawnedEnemyBullets = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        bulletSpawnPoint = transform.position; // Fixed to enemy ship's position

        // Call Shoot() every fireInterval (1s by default)
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            Shoot();
            fireTimer = 0f; // Reset timer
        }
    }

    // Spawn enemy bullet and keep track of them
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint, Quaternion.identity);
        spawnedEnemyBullets.Add(bullet);
    }

    // Clear all spawned enemy bullets and convert to score
    public void ExplodeBullets()
    {
        foreach (GameObject bullet in spawnedEnemyBullets)
        {
            if (bullet != null)
            {
                UI.instance.AddScore(10);
                Destroy(bullet);
            }
        }
    }
}
