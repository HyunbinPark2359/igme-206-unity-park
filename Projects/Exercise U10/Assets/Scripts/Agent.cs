using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Agent : MonoBehaviour
{
    protected PhysicsObject myPhysicsObject;
    protected Vector3 totalForce = Vector3.zero;

    [SerializeField] private float wanderAngle = 0.0f;
    [SerializeField] private float maxWanderAngle = 45.0f;
    [SerializeField] private float maxWanderChangePerSecond = 45.0f;

    // Obstacles in the scene
    [SerializeField] private List<Obstacle> obstacles = new List<Obstacle>();

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

    public Vector3 AvoidObstacle(float weight = 1.0f)
    {
        Vector3 avoidingForce = Vector3.zero;   // Avoid steering force
        Vector3 desiredVelocity = Vector3.zero; // Right or left of the agent
        List<Obstacle> obstaclesInPath = new List<Obstacle>(); // Obstacles in the agent's path
        Obstacle closestObstacle = null;        // The closest obstacle in the agent's path
        float closestSqrDistance = -1.0f;       // Agent's distance to the closest obstacle on its path

        // Iterate through the obstacles to determine if it's in the agent's path
        foreach (Obstacle obstacle in obstacles)
        {
            // obstacle.GetComponent<SpriteRenderer>().color = Color.white;

            // Agent's distance to the obstacle
            Vector3 toObstacle = obstacle.Position - this.Position;
            float dotProduct = Vector3.Dot(this.myPhysicsObject.velocity.normalized, toObstacle.normalized);
            if (dotProduct < 0) // Dot product is negative when the object is behind the agent
            {
                continue; // This obstacle is not in the path, move on to next obstacle
            }

            float unitAway = 3.0f;
            if (toObstacle.sqrMagnitude > Mathf.Pow(unitAway, 2)) // The obstacle is far enough from the agent
            {
                continue; // This obstacle is not in the path, move on to next obstacle
            }

            dotProduct = Vector3.Dot(this.transform.right, toObstacle);
            if (this.myPhysicsObject.Radius + obstacle.Radius < Mathf.Abs(dotProduct)) // Reynold's test for non-intersection
            {
                continue; // This obstacle is not in the path, move on to next obstacle
            }

            // Add this obstacle to the list
            obstaclesInPath.Add(obstacle);

            // Update the closestObstacle if this one is the closest
            if (closestSqrDistance < 0 || closestSqrDistance > toObstacle.sqrMagnitude)
            {
                closestSqrDistance = toObstacle.sqrMagnitude;
                closestObstacle = obstacle;
            }
        }

        /* for debugging
        foreach(Obstacle obstacle in obstaclesInPath)
        {
            obstacle.GetComponent<SpriteRenderer>().color = Color.red;
        }
        */

        // Determine the avoid steering force from the closestObstacle
        if (closestObstacle != null)
        {
            Vector3 toClosest = closestObstacle.Position - this.Position;
            float dotProduct = Vector3.Dot(this.transform.right, toClosest);

            if (dotProduct > 0) // Obstacle on the right
            {
                desiredVelocity = -this.transform.right * myPhysicsObject.maxSpeed; // Turn left
            }
            else // Obstacle either on the left or in front
            {
                desiredVelocity = this.transform.right * myPhysicsObject.maxSpeed; // Turn right
            }
            avoidingForce = desiredVelocity - myPhysicsObject.velocity;

            // Change the direction of avoidingForce
            // if it leads the agent to move out of the screen
            // The movement bugs when the futurePosition is too long
            Vector3 futurePosition = this.Position + avoidingForce.normalized * 0.3f;
            if (futurePosition.x < myPhysicsObject.screenLeft || futurePosition.x > myPhysicsObject.screenRight ||
                futurePosition.y < myPhysicsObject.screenBottom || futurePosition.y > myPhysicsObject.screenTop)
            {
                avoidingForce.x = -avoidingForce.x;
                avoidingForce.y = -avoidingForce.y;
            }

            // Very close obstacle increases the weight of the force
            if (toClosest.sqrMagnitude < 1.0f)
            {
                weight *= Mathf.Clamp((1 / toClosest.sqrMagnitude), 1, 5);
            }
        }

        return avoidingForce * weight;
    }

    /*
    void OnDrawGizmos()
    {
        if (obstacles == null || obstacles.Count == 0) return;

        foreach (Obstacle obstacle in obstacles)
        {
            Vector3 toObstacle = obstacle.Position - this.Position;
            float distance = toObstacle.magnitude;

#if UNITY_EDITOR
            // Draw distance label
            //Handles.Label(obstacle.Position, $"Distance: {distance:F2}");
            // Draw velocity label
            Handles.Label(this.Position, $"{myPhysicsObject.velocity.magnitude:F2}");
#endif
        }
    }
    */
}