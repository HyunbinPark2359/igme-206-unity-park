using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionManager : MonoBehaviour
{
    // SpriteInfo of player ship
    [SerializeField] private SpriteInfo player;
    
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
                if (CircleCollision(player, enemyBulletInfo))
                {
                    Destroy(bullet);
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
                if (CircleCollision(player, enemyShipInfo))
                {
                    Destroy(ship);
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
                        }
                    }
                }
            }
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
}
