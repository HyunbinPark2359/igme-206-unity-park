using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    private Vector3 objectPosition;
    private Vector3 velocity;
    [SerializeField] private float moveSpeed = 0.2f;
    [SerializeField] private float lateralSpeed = 2.0f;

    private bool movingRight;
    private float randNum; // RNG for ship's movement

    // Start is called before the first frame update
    void Start()
    {
        // Enemy's ship is facing downwards
        transform.rotation = Quaternion.Euler(0, 0, 180);

        // Get a random number to decide ship's movement
        randNum = Random.Range(0.0f, 1.0f);
        moveSpeed += randNum * 2;
        lateralSpeed += randNum * 2;
        if (randNum < 0.5)
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;
        }

        objectPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the ship
        objectPosition = transform.position;
        velocity = Vector3.down * moveSpeed * Time.deltaTime;

        // Add ship's lateral movement to its velocity
        // and change its direction upon it reaches the side edges
        if (movingRight)
        {
            velocity += Vector3.right * lateralSpeed * Time.deltaTime;
            if (objectPosition.x > MovementController.screenRight) // Right boundary
            {
                movingRight = false;
                objectPosition.x = MovementController.screenRight;
            }
        }
        else
        {
            velocity += Vector3.left * lateralSpeed * Time.deltaTime;
            if (objectPosition.x < MovementController.screenLeft) // Left boundary
            {
                movingRight = true;
                objectPosition.x = MovementController.screenLeft;
            }
        }

        objectPosition += velocity;
        transform.position = objectPosition;

        // Destroy the ship upon it exits the screen
        if (objectPosition.y < MovementController.screenBottom)
        {
            Destroy(gameObject);
        }
    }
}
