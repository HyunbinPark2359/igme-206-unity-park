using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class MouseTracker : MonoBehaviour
{
    private PhysicsObject myPhysicsObject;

    void Start()
    {
        myPhysicsObject = GetComponent<PhysicsObject>();
    }

    void Update()
    {
        // Get the screen coordinates of the mouse
        Vector3 mousePositionInScreen = Mouse.current.position.ReadValue();

        // Convert them to world coordinates
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionInScreen.x, mousePositionInScreen.y, 0));

        // Make sure Z value is 0
        mousePositionInWorld.z = 0;

        // Apply a force
        myPhysicsObject.acceleration += (mousePositionInWorld - transform.position) / myPhysicsObject.mass;
    }
}
