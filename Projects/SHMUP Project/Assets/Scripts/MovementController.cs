using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Fields for vehicle movement
    private Vector3 objectPosition;
    private Vector3 direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    private float speed = 7f;

    // Fields for screen wrapping
    private float screenLeft;
    private float screenRight;
    private float screenTop;
    private float screenBottom;

    // Set property for direction
    public Vector3 Direction
    {
        set
        {
            direction = value.normalized;
        }
    }

    private void Start()
    {
        // Initialize the GameObject¡¯s position
        objectPosition = transform.position;

        // Calculate screen boundaries
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        // Initialize fields
        screenLeft = screenBottomLeft.x;
        screenRight = screenTopRight.x;
        screenBottom = screenBottomLeft.y;
        screenTop = screenTopRight.y;
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
        if (objectPosition.x < screenLeft) // Left boundary
        {
            objectPosition.x = screenLeft;
        }
        else if (objectPosition.x > screenRight) // Right boundary
        {
            objectPosition.x = screenRight;
        }

        if (objectPosition.y < screenBottom) // Bottom boundary
        {
            objectPosition.y = screenBottom;
        }
        else if (objectPosition.y > screenTop) // Top boundary
        {
            objectPosition.y = screenTop;
        }

        // Update position after wrapping
        transform.position = objectPosition;
    }
}