using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceExample : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 forceDirection = Vector3.forward;
    public float forceMagnitude = 10f;

    void Start()
    {
        // Add force to the Rigidbody when the script starts
        rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
    }
}
