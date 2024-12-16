using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public PhysicsObject myPhysicsObject;
    private Vector3 totalForce = Vector3.zero;
    [SerializeField] private GameObject player;

    void Awake()
    {
        // Enemy's bullet is facing downwards
        transform.rotation = Quaternion.Euler(0, 0, 180);

        myPhysicsObject = GetComponent<PhysicsObject>();

        // Enemy's bullets move downwards
        myPhysicsObject.Direction = Vector3.down;
    }

    void Update()
    {
        // Find and apply a seeking force to the player ship
        totalForce = Vector3.zero;
        CalcSteeringForces();
        myPhysicsObject.ApplyForce(totalForce);

        // Destroy the bullet when it exits the screen
        if (myPhysicsObject.Position.x < ScreenDetector.ScreenLeft ||
            myPhysicsObject.Position.x > ScreenDetector.ScreenRight ||
            myPhysicsObject.Position.y < ScreenDetector.ScreenBottom ||
            myPhysicsObject.Position.y > ScreenDetector.ScreenTop)
        {
            MissileSpawner.instance.DeactivateMissile(gameObject);
        }
    }

    public void CalcSteeringForces()
    {
        totalForce += Seek(player);
    }

    // Method to find seek steering force
    public Vector3 Seek(Vector3 targetPos)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = targetPos - myPhysicsObject.position;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * myPhysicsObject.maxSpeed;

        // Calculate seek steering force
        Vector3 seekingForce = desiredVelocity - myPhysicsObject.velocity;

        // Return seek steering force
        return seekingForce;
    }

    public Vector3 Seek(GameObject target)
    {
        return Seek(target.transform.position);
    }
}
