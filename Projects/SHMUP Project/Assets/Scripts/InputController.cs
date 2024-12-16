using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    private bool canShoot = true;
    private float fireCooldown = 0.2f;
    private float cooldownTimer = 0.0f;

    private void Start()
    {
        myMovementController = GetComponent<MovementController>();
    }

    private void Update()
    {
        // bulletSpawnPoint is fixed on player's position
        bulletSpawnPoint = transform.position;

        if (!canShoot)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer > fireCooldown)
            {
                cooldownTimer = 0.0f;
                canShoot = true;
            }
        }
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
        if (context.performed && !CollisionManager.instance.IsInvincible && canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint, Quaternion.identity);
            spawnedPlayerBullets.Add(bullet);
            canShoot = false;
        }
    }

    // Call ActivateAbility() method
    public void Ability(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CollisionManager.instance.ActivateAbility();
        }
    }

    // Clear all enemy ships and bullets in the field
    public void Bomb(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UI.instance.UseBomb();
        }
    }
}