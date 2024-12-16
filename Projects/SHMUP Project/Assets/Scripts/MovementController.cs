using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public static MovementController instance { get; private set; }

    // Fields for vehicle movement
    private Vector3 objectPosition;
    private Vector3 direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float speed = 8f;

    // Set property for direction
    public Vector3 Direction
    {
        set
        {
            direction = value.normalized;
        }
    }

    // Get/Set property for speed
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    private void Awake()
    {
        instance = this;

        // Initialize the GameObject¡¯s position
        objectPosition = transform.position;
    }

    void Update()
    {
        // Move the vehicle
        velocity = direction * speed * Time.deltaTime;
        objectPosition += velocity;
        transform.position = objectPosition;

        // Wrap around screen edges
        BoundariesAroundTheEdges();
    }

    private void BoundariesAroundTheEdges()
    {
        if (objectPosition.x < ScreenDetector.ScreenLeft) // Left boundary
        {
            objectPosition.x = ScreenDetector.ScreenLeft;
        }
        else if (objectPosition.x > ScreenDetector.ScreenRight) // Right boundary
        {
            objectPosition.x = ScreenDetector.ScreenRight;
        }

        if (objectPosition.y < ScreenDetector.ScreenBottom) // Bottom boundary
        {
            objectPosition.y = ScreenDetector.ScreenBottom;
        }
        else if (objectPosition.y > ScreenDetector.ScreenTop) // Top boundary
        {
            objectPosition.y = ScreenDetector.ScreenTop;
        }

        // Update position after wrapping
        transform.position = objectPosition;
    }
}