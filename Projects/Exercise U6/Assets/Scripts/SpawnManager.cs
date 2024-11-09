using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    // (Optional) Prevent non-singleton constructor use.
    protected SpawnManager() { }

    // Fields refer to 
    [SerializeField] private List<Sprite> sprites;  // the sprite images
    [SerializeField] private GameObject prefab;     // a prefab for a creature
    // Keep track of the GameObjects created
    private List<GameObject> spawnedCreatures = new List<GameObject>();

    // Fields for gaussian
    private float meanX = 0f;
    private float stdDevX = 3f;
    private float meanY = 0f;
    private float stdDevY = 3f;

    // Spawn creatures automatically as the game starts
    void Start()
    {
        Spawn();
    }

    // Spawn uniform random number of creatures
    public void Spawn()
    {
        CleanUp();

        int randNumCreatures = Random.Range(10, 20);
        // Add spawned creatures into the list to keep track of them
        for (int i = 0; i < randNumCreatures; i++)
        {
            spawnedCreatures.Add(SpawnCreature());
        }
    }

    // Destroy all GameObjects in the game and old creatures
    public void CleanUp()
    {
        foreach (GameObject creature in spawnedCreatures)
        {
            Destroy(creature);
        }

        spawnedCreatures.Clear();
    }

    // Choose which creature to spawn and give it a random color
    // Its position vector is also randomly assigned by Gaussian function
    public GameObject SpawnCreature()
    {
        // Get standard deviation which is width or height of orthographic camera divided by 4
        // With this value of std dev, 100% of creatures will fall within camera's view(4 standard deviations) from the origin(mean)
        Camera cam = Camera.main;
        stdDevX = stdDevY * cam.aspect;
        stdDevY = cam.orthographicSize * 2f / 8;

        GameObject newCreature = Instantiate(prefab, new Vector2(Gaussian(meanX, stdDevX), Gaussian(meanY, stdDevY)), Quaternion.identity);
        newCreature.GetComponent<SpriteRenderer>().sprite = ChooseRandomCreature();

        Color randomColor = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        newCreature.GetComponent<SpriteRenderer>().color = randomColor;

        return newCreature;
    }

    // Choose non-uniform random creature
    public Sprite ChooseRandomCreature()
    {
        int randNum = PickRandomObject();

        return sprites[randNum];
    }

    // Gaussian function
    private float Gaussian(float mean, float stdDev)
    {
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);

        float gaussValue =
        Mathf.Sqrt(-2.0f * Mathf.Log(val1)) *
        Mathf.Sin(2.0f * Mathf.PI * val2);

        return mean + stdDev * gaussValue;
    }

    // Pick random floating number to decide which creature
    public int PickRandomObject()
    {
        float randomChance = Random.Range(0.0f, 1.0f);
        if (randomChance < 0.25)
        {
            return 0;   // Elephant 25%
        }
        else if (randomChance < 0.45)
        {
            return 1;   // Turtle 20%
        }
        else if (randomChance < 0.6)
        {
            return 2;   // Snail 15%
        }
        else if (randomChance < 0.7)
        {
            return 3;   // Octopus 10%
        }
        else
        {
            return 4;   // Kangarro 30%
        }
    }
}
