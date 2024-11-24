using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Agent
{
    public float stayInBoundsWeight = 3.0f;
    public float wanderWeight = 1.0f;

    public override void CalcSteeringForces()
    {
        totalForce += Wander(wanderWeight);
        StayInBounds(stayInBoundsWeight);
    }
}
