using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void Start()
    {
        base.Start();

        moveSpeed = 8.0f;
        // Player's bullets move upwards
        bulletDirection = Vector3.up;
    }

    protected override void Update()
    {
        base.Update();

        // Destroy the bullet when it exits the screen
        if (objectPosition.y > MovementController.screenTop)
        {
            Destroy(gameObject);
        }
    }
}