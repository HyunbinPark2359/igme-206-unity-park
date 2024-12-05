using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private Vector2 inputDirection;
    private MovementController myMovementController;

    // Fields for shooting player's bullets
    public GameObject bulletPrefab;
    private Vector3 bulletSpawnPoint;
    public static List<GameObject> spawnedPlayerBullets = new List<GameObject>();

    private void Start()
    {
        myMovementController = GetComponent<MovementController>();
    }

    private void Update()
    {
        // bulletSpawnPoint is fixed on player's position
        bulletSpawnPoint = transform.position;
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

    // Spawn bullets and keep track of those objects
    public void Shoot(InputAction.CallbackContext context)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint, Quaternion.identity);
        spawnedPlayerBullets.Add(bullet);
    }
}