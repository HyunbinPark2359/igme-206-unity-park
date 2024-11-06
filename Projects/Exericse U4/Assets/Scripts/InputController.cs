using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private Vector2 inputDirection;
    private MovementController myMovementController;

    private void Start()
    {
        myMovementController = GetComponent<MovementController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Get the latest value for the input from the Input System
        inputDirection = context.ReadValue<Vector2>();  // This is already normalized for us

        // Send that new direction to the Vehicle class
        if (myMovementController != null)
        {
            myMovementController.Direction = inputDirection;
        }
    }
}
