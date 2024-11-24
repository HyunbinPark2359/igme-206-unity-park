using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Agent : MonoBehaviour
{
    public PhysicsObject myPhysicsObject;

    public Vector3 totalForce = Vector3.zero;

    protected float wanderAngle = 0.0f;
    public float maxWanderAngle = 45.0f;
    public float maxWanderChangePerSecond = 45.0f;

    public Vector3 Position
    {
        get
        {
            return myPhysicsObject.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myPhysicsObject = GetComponent<PhysicsObject>();
    }

    // Update is called once per frame
    void Update()
    {
        totalForce = Vector3.zero;

        CalcSteeringForces();

        myPhysicsObject.ApplyForce(totalForce);
    }

    // Abstract method to calculate steering force applied to the agent
    public abstract void CalcSteeringForces();

    // Method to find seek steering force
    public Vector3 Seek(Vector3 targetPos, float weight = 1.0f)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = targetPos - myPhysicsObject.position;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * myPhysicsObject.maxSpeed;

        // Calculate seek steering force
        Vector3 seekingForce = desiredVelocity - myPhysicsObject.velocity;

        // Return seek steering force
        return seekingForce * weight;
    }

    // Method to find flee steering force
    public Vector3 Flee(Vector3 targetPos)
    {
        Vector3 desiredVelocity = myPhysicsObject.position - targetPos;

        desiredVelocity = desiredVelocity.normalized * myPhysicsObject.maxSpeed;

        Vector3 fleeingForce = desiredVelocity - myPhysicsObject.velocity;

        return fleeingForce;
    }

    // Seek method with Agent parameter
    public Vector3 Seek(Agent target)
    {
        return Seek(target.transform.position);
    }

    // Flee method with Agent parameter
    public Vector3 Flee(Agent target)
    {
        return Flee(target.transform.position);
    }


    public Vector3 Wander(float weight = 1.0f)
    {
        // Update the angle of our current wander
        float maxWanderChange = maxWanderChangePerSecond * Time.deltaTime;
        wanderAngle += Random.Range(-maxWanderChange, maxWanderChange);

        wanderAngle = Mathf.Clamp(wanderAngle, -maxWanderAngle, maxWanderAngle);

        // Get a position that is defined by that wander angle
        Vector3 targetPos = Quaternion.Euler(0, 0, wanderAngle) * myPhysicsObject.direction.normalized + myPhysicsObject.position;

        // Seek towards our wander position
        return Seek(targetPos, weight);
    }

    public Vector3 GetFuturePosition(float secondsToLookAhead = 1.0f)
    {
        return myPhysicsObject.position + myPhysicsObject.velocity * secondsToLookAhead;
    }

    public void StayInBounds(float weight = 1.0f)
    {
        Vector3 futurePosition = GetFuturePosition();

        if (futurePosition.x < myPhysicsObject.screenLeft || futurePosition.x > myPhysicsObject.screenRight ||
            futurePosition.y < myPhysicsObject.screenBottom || futurePosition.y > myPhysicsObject.screenTop)
        {
            totalForce += Seek(Vector3.zero, weight);
        }
    }
}
