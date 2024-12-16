using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShip : EnemyShip
{
    [SerializeField] private int health = 50;

    public int Health {  get { return health; } set {  health = value; } }

    private void Start()
    {
        // Enemy's ship is facing downwards
        transform.rotation = Quaternion.Euler(0, 0, 180);

        lateralSpeed = 0;

        transform.position = new Vector3(0, ScreenDetector.ScreenTop + 1.1f, 0);
        objectPosition = transform.position;
    }

    private void Update()
    {
        // Move to specific position after it spawns
        objectPosition = Vector3.MoveTowards(transform.position, new Vector3(0, ScreenDetector.ScreenTop - 2.5f, 0), moveSpeed * Time.deltaTime);
        transform.position = objectPosition;
    }
}
