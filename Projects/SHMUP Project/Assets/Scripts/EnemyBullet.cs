using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void Start()
    {
        base.Start();

        // Enemy's bullets move downwards
        bulletDirection = Vector3.down;
    }

    protected override void Update()
    {
        base.Update();

        // Destroy the bullet when it exits the screen
        if (objectPosition.y < MovementController.screenBottom)
        {
            Destroy(gameObject);
        }
    }
}
