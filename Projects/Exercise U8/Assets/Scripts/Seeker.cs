using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Agent
{
    // Reference to Agent which Seeker seeks for
    public Agent agentToSeek;

    // Override method from Agent
    public override void CalcSteeringForces()
    {
        // Find a steering force and add it to totalForce
        totalForce += Seek(agentToSeek.Position);

        // Teleport the fleer to a random position upon their collision
        if (CircleCollision(myPhysicsObject, agentToSeek.myPhysicsObject))
        {
            agentToSeek.myPhysicsObject.position = new Vector3
                (Random.Range(myPhysicsObject.screenLeft, myPhysicsObject.screenRight),
                 Random.Range(myPhysicsObject.screenBottom, myPhysicsObject.screenTop), 0);
        }
    }

    // Check if Seeker has collided with its target
    public bool CircleCollision(PhysicsObject a, PhysicsObject b)
    {
        float distance = Vector2.Distance(a.transform.position, b.transform.position);
        return distance < a.Radius + b.Radius;
    }
}
