using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public static MissileSpawner instance { get; private set; }

    [SerializeField] private GameObject[] missiles;

    [SerializeField] private Transform missileSpawnPoint_0;
    [SerializeField] private Transform missileSpawnPoint_1;
    [SerializeField] private float fireInterval = 3.3f;
    private float fireTimer;
    public static List<GameObject> spawnedMissiles = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Call Shoot() every fireInterval (1s by default)
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            Shoot();
            fireTimer = 0f; // Reset timer
        }
    }

    // Spawn missile and keep track of them
    public void Shoot()
    {
        int i = FindMissile();
        if (i == -1)
        {
            return;
        }
        missiles[i].SetActive(true);
        missiles[i].GetComponent<Missile>().myPhysicsObject.Position = missileSpawnPoint_0.transform.position;
        spawnedMissiles.Add(missiles[i]);

        i = FindMissile();
        if (i == -1)
        {
            return;
        }
        missiles[i].SetActive(true);
        missiles[i].GetComponent<Missile>().myPhysicsObject.Position = missileSpawnPoint_1.transform.position;
        spawnedMissiles.Add(missiles[i]);
    }

    // Choose which missile to spawn
    public int FindMissile()
    {
        for (int i = 0; i < missiles.Length; i++)
        {
            if (!missiles[i].activeInHierarchy)
            {
                return i;
            }
        }

        return -1;
    }

    // Despawn missile and reset its position, direction, and velocity
    public void DeactivateMissile(GameObject missile)
    {
        missile.transform.position = new Vector3(0, 10, 0);
        missile.GetComponent<PhysicsObject>().Direction = Vector3.down;
        missile.GetComponent<PhysicsObject>().Velocity = Vector3.zero;
        missile.SetActive(false);
    }

    // Clear all spawned missiles and convert to score
    public void ExplodeMissiles()
    {
        foreach (GameObject missile in spawnedMissiles)
        {
            if (missile != null)
            {
                UI.instance.AddScore(20);
                DeactivateMissile(missile);
            }
        }
    }
}
