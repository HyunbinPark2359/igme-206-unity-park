using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static CollisionManager instance { get; private set; }

    // SpriteInfo of player ship
    [SerializeField] private SpriteInfo player;
    [SerializeField] private GameObject boss;
    private SpriteInfo bossSprite;

    public int scoreValue = 50; // Value of enemy ship

    private bool isInvincible = false;
    private float abilityTimer = 0.0f;
    private bool abilityToken = true;   // To use ability

    private bool isPlayerHit = false;
    private float playerHitTimer = 0.0f;

    private bool isBossHit = false;
    private float bossHitTimer = 0.0f;

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
    public bool AbilityToken { set  { isPlayerHit = value; } }

    private void Awake()
    {
        instance = this;

        bossSprite = boss.GetComponent<SpriteInfo>();
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
                    isPlayerHit = true;
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
                    isPlayerHit = true;
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

        // Detect collision between player bullet and boss
        foreach (GameObject bullet in InputController.spawnedPlayerBullets)
        {
            if (bullet != null)
            {
                // SpriteInfo of player's bullet in the scene
                SpriteInfo playerBulletInfo = bullet.GetComponent<SpriteInfo>();

                // Destroy the bullet if collided with boss
                if (CircleCollision(playerBulletInfo, bossSprite) && !isInvincible)
                {
                    Destroy(bullet);
                    isBossHit = true;
                    boss.GetComponent<BossShip>().Health--;
                }
            }
        }

        // Detect collision between player ship and missile
        foreach (GameObject missile in MissileSpawner.spawnedMissiles)
        {
            if (missile != null)
            {
                // SpriteInfo of missile in the scene
                SpriteInfo missileInfo = missile.GetComponent<SpriteInfo>();

                // Destroy the missile if collided with player
                if (CircleCollision(player, missileInfo) && !isInvincible)
                {
                    MissileSpawner.instance.DeactivateMissile(missile);
                    UI.instance.TakeDamage();
                    isPlayerHit = true;
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


        if (isPlayerHit)
        {
            ShowPlayerHitEffect();
        }

        if (isBossHit)
        {
            ShowBossHitEffect();
        }

        //Debug.Log(abilityToken);
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
        if (abilityToken && !IsInvincible)
        {
            abilityToken = false;
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

    // Change player sprite color to red for 0.1 second upon its hit
    public void ShowPlayerHitEffect()
    {
        playerHitTimer += Time.deltaTime;
        player.GetComponent<SpriteRenderer>().color = Color.red;

        if (playerHitTimer > 0.1f)
        {
            playerHitTimer = 0.0f;
            player.GetComponent<SpriteRenderer>().color = Color.white;
            isPlayerHit = false;
        }
    }

    // Change boss sprite color to red for 0.1 second upon its hit
    public void ShowBossHitEffect()
    {
        bossHitTimer += Time.deltaTime;
        boss.GetComponent<SpriteRenderer>().color = Color.red;

        if (bossHitTimer > 0.1f)
        {
            bossHitTimer = 0.0f;
            boss.GetComponent<SpriteRenderer>().color = Color.white;
            isBossHit = false;
        }
    }
}
