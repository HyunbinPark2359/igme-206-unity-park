using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    // Reference to creature prefab
    public GameObject creaturePrefab;

    // Locations to spawn 3 creatures
    public Vector3 spawnLocation1;
    public Vector3 spawnLocation2;
    public Vector3 spawnLocation3;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate 3 copies of creature prefab with different locations
        Instantiate(creaturePrefab, spawnLocation1, Quaternion.identity);
        Instantiate(creaturePrefab, spawnLocation2, Quaternion.identity);
        Instantiate(creaturePrefab, spawnLocation3, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
