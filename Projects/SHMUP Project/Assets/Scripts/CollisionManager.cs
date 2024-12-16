using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static CollisionManager instance { get; private set; }

    // SpriteInfo of player ship
    [SerializeField] private SpriteInfo player;

    public int scoreValue = 50;
    private bool isInvincible = false;
    private float abilityTimer = 0.0f;
    private int abilityToken = 1;

    public bool IsInvincible
    {
        get
        {
            return isInvincible;
        }

        set
        {
            isInvincible = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Detect collision between player ship and enemy bullet
        foreach (GameObject bullet in BulletSpawner.spawnedEnemyBullets)
        {
            if (bullet != null)
            {
                // SpriteInfo of enemy's bullet in the scene
                SpriteInfo enemyBulletInfo = bullet.GetComponent<SpriteInfo>();

                // Destroy the bullet if collided with player
                if (CircleCollision(player, enemyBulletInfo) && !isInvincible)
                {
                    Destroy(bullet);
                    UI.instance.TakeDamage();
                }
            }
        }

        foreach (GameObject ship in EnemySpawner.spawnedEnemyShips)
        {
            if (ship != null)
            {
                // SpriteInfo of enemy's ship in the scene
                SpriteInfo enemyShipInfo = ship.GetComponent<SpriteInfo>();

                // Detect collision between enemy ship and player ship
                // Destroy enemy ship if collided with player
                if (CircleCollision(player, enemyShipInfo) && !isInvincible)
                {
                    Destroy(ship);
                    UI.instance.TakeDamage();
                }

                // Detect collision between enemy ship and player's bullet
                foreach (GameObject bullet in InputController.spawnedPlayerBullets)
                {
                    if (bullet != null) // Check if the bullet still exists
                    {
                        // SpriteInfo of player's bullet in the scene
                        SpriteInfo playerBulletInfo = bullet.GetComponent<SpriteInfo>();

                        // Destory enemy ship and player's bullet if collided
                        if (CircleCollision(enemyShipInfo, playerBulletInfo))
                        {
                            Destroy(ship);
                            Destroy(bullet);
                            UI.instance.AddScore(scoreValue);
                        }
                    }
                }
            }
        }

        // One second timer for special ability
        if (isInvincible)
        {
            abilityTimer += Time.deltaTime;
        }
        if (abilityTimer > 1)
        {
            abilityTimer = 0.0f;
            DeactivateAbility();
        }
    }

    // AABB Collision Detection
    bool AABBCollision(SpriteInfo a, SpriteInfo b)
    {
        return a.MinX < b.MaxX && a.MaxX > b.MinX && a.MaxY > b.MinY && a.MinY < b.MaxY;
    }

    // Bounding Circle Collision Detection
    bool CircleCollision(SpriteInfo a, SpriteInfo b)
    {
        float distance = Vector2.Distance(a.transform.position, b.transform.position);
        return distance < a.Radius + b.Radius;
    }

    // Activate special ability
    public void ActivateAbility()
    {
        if (abilityToken > 0 && !IsInvincible)
        {
            abilityToken--;
            isInvincible = true;
            MovementController.instance.Speed *= 2;
            player.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    // Deactivate special ability
    public void DeactivateAbility()
    {
        isInvincible = false;
        MovementController.instance.Speed /= 2;
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
