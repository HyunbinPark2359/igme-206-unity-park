using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    // (Optional) Prevent non-singleton constructor use.
    protected SpawnManager() { }

    [SerializeField]
    private List<Sprite> sprites;
    private GameObject prefab;

    private List<GameObject> spawnedCreatures = new List<GameObject>();

    public float meanX = 0f;
    public float stdDevX = 2f;
    public float meanY = 0f;
    public float stdDevY = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Attached this script to Button Click in Unity Scene.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCreature()
    {
        int randNum = Random.Range(0, sprites.Count);
        // Sprite chosenSprite = 

        GameObject newCreature = Instantiate(prefab, new Vector2(Gaussian(meanX, stdDevX), Gaussian(meanY, stdDevY), Quaternion.identity));
        newCreature.GetComponent<SpriteRenderer>().sprite = chosenSprite;
    }

    public void Spawn()
    {
        int randNumCreatures = Random.Range(5, 15);

        for (int i = 0; i < randNumCreatures; i++)
        {
            SpawnCreature();
        }
    }    
}
