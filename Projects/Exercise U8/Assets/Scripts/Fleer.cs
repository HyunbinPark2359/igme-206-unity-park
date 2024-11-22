using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleer : Agent
{
    // Reference to Agent which Fleer flees from
    public Agent agentToFleeFrom;

    // Override method from Agent
    public override void CalcSteeringForces()
    {
        // Find a steering force and add it to totalForce
        totalForce += Flee(agentToFleeFrom.Position);
    }
}
