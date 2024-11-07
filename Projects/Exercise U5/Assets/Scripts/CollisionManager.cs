using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionManager : MonoBehaviour
{
    private enum CollisionMode
    {
        AABB,
        Circle
    }
    private CollisionMode currentCollisionMode;

    public SpriteInfo vehicle;
    public List<SpriteInfo> collidableObjects = new List<SpriteInfo>();
    public TextMesh instruction;

    void Start()
    {
        // Set default collision mode to AABB
        currentCollisionMode = CollisionMode.AABB;

        // Display the instruction
        instruction.text = "Left Click to Change Mode\n\nCurrent Collision Mode: " + currentCollisionMode.ToString();
    }

    void Update()
    {
        // Switch collision mode on left click
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (currentCollisionMode == CollisionMode.AABB)
            {
                currentCollisionMode = CollisionMode.Circle;
            }
            else
            {
                currentCollisionMode = CollisionMode.AABB;
            }

            // Update the instruction
            instruction.text = "Left Click to Change Mode\n\nCurrent Collision Mode: " + currentCollisionMode.ToString();
        }

        bool vehicleColliding = false;  // Track if the vehicle is colliding

        // Check for collisions for each object
        foreach (SpriteInfo collidableObject in collidableObjects)
        {
            bool isColliding;   // Track if the object is colliding

            // Check if the object is colliding based on current collision mode
            if (currentCollisionMode == CollisionMode.AABB)
            {
                isColliding = AABBCollision(vehicle, collidableObject);
            }
            else
            {
                isColliding = CircleCollision(vehicle, collidableObject);
            }

            // Change object color based on its collision
            if (isColliding)
            {
                collidableObject.GetComponent<SpriteRenderer>().color = Color.red;
                vehicleColliding = true;    // Since collision is detected, set vehicle collision to true
            }
            else
            {
                collidableObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        // Change vehicle color based on its collision
        if (vehicleColliding)
        {
            vehicle.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            vehicle.GetComponent<SpriteRenderer>().color = Color.white;
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
