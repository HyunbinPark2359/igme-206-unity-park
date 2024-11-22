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
    public bool useFriction = false;
    public bool useGravity = false;
    public float coefFriction;
    public float gravityStrength = 1.0f;
    public float maxSpeed = 20.0f;

    // Fields for screen boundary
    public float screenLeft;
    public float screenRight;
    public float screenTop;
    public float screenBottom;

    // Radius to check for CircleToCircle collision
    private SpriteRenderer mySpriteRenderer;
    public float Radius
    {
        get
        {
            if (mySpriteRenderer.bounds.extents.x > mySpriteRenderer.bounds.extents.y)
            {
                return mySpriteRenderer.bounds.extents.x;
            }
            else
            {
                return mySpriteRenderer.bounds.extents.y;
            }
        }
    }

    void Start()
    {
        position = transform.position;

        mySpriteRenderer = GetComponent<SpriteRenderer>();

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
        // Calculate acceleration
        ApplyGravity();
        ApplyFriction();

        // Calculate velocity of the object capped at maxSpeed
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        position += velocity * Time.deltaTime;

        // Get current direction from velocity
        direction = velocity.normalized;

        Bounce();

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


    // Apply a force for friction to the object
    public void ApplyFriction()
    {
        if (useFriction)
        {
            Vector3 friction = velocity * -1;
            friction.Normalize();
            friction = friction * this.coefFriction;
            ApplyForce(friction);
        }
    }

    // Apply a force for gravity to the object
    public void ApplyGravity()
    {
        if (useGravity)
        {
            Vector3 gravity = Vector3.down * gravityStrength * this.mass;
            ApplyForce(gravity);
        }
    }

    // Bounce the object around inside the window
    public void Bounce()
    {
        // 10% of its velocity is lost when bound
        float bounce = -0.9f;

        if (this.position.x < screenLeft)       // Left boundary
        {
            this.position.x = screenLeft;
            this.velocity.x *= bounce;
        }
        else if (this.position.x > screenRight) // Right boundary
        {
            this.position.x = screenRight;
            this.velocity.x *= bounce;
        }
        if (this.position.y < screenBottom)     // Bottom boundary
        {
            this.position.y = screenBottom;
            this.velocity.y *= bounce;
        }
        else if (this.position.y > screenTop)   // Top boundary
        {
            this.position.y = screenTop;
            this.velocity.y *= bounce;
        }
    }
}
