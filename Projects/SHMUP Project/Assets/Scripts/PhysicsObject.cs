using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    // Fields for physics object
    public Vector3 position;
    public Vector3 direction;
    public Vector3 velocity;
    public Vector3 acceleration;
    public float mass = 1.0f;

    // Fields to facilitate movement and forces
    public float maxSpeed = 5.0f;

    public Vector3 Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }
    public Vector3 Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }
    public Vector3 Velocity
    {
        set
        {
            velocity = value;
        }
    }

    void Awake()
    {
        position = transform.position;
    }

    void Update()
    {
        // Calculate velocity of the object capped at maxSpeed
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        position += velocity * Time.deltaTime;

        // Get current direction from velocity
        direction = velocity.normalized;

        // Update its position
        transform.position = position;

        // Rotate the object to face its current direction
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Zero out acceleration
        acceleration = Vector3.zero;
    }

    // Apply a force to accelerate
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }
}