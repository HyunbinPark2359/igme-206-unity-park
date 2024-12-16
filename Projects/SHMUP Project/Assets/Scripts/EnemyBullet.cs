using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : Bullet
{
    protected override void Start()
    {
        base.Start();

        // Enemy's bullet is facing downwards
        transform.rotation = Quaternion.Euler(0, 0, 180);

        // Enemy's bullets move downwards
        bulletDirection = Vector3.down;
    }

    protected override void Update()
    {
        base.Update();

        // Destroy the bullet when it exits the screen
        if (objectPosition.y < ScreenDetector.ScreenBottom)
        {
            Destroy(gameObject);
        }
    }
}
