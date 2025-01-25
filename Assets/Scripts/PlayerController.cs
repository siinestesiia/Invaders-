using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Projectile projectile;
    public event Action OnShooting;
    public event Action OnStopShooting;

    bool isShooting = false;

    PlayerInput playerInput;
    
    InputAction moveAction;
    InputAction shootAction;

    private Vector2 mousePosition;
    private float cameraHeight; // From top view.
    
    [Header ("World Space Boundaries for the Spaceship movement.")]
    [SerializeField] float minX = -28;
    [SerializeField] float maxX = 28;
    [SerializeField] float minZ = -15;
    [SerializeField] float maxZ = 14;

    float movementSmoothness = 13f; // Higher value = faster movement.

    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        moveAction = playerInput.actions["move"];
        shootAction = playerInput.actions["Shoot"];

        cameraHeight = Camera.main.transform.position.y;
    }

    void Update()
    {
        SetPlayerPosition();
        ShootWeapons();
    }


    // Spaceship Movement ---------------------------------------------------- //
    private void SetPlayerPosition()
    {
        mousePosition = moveAction.ReadValue<Vector2>();
        
        Vector3 screenPoint = new Vector3(mousePosition.x, mousePosition.y, cameraHeight);
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(screenPoint);

        // Calculate the clamped target position (for the mouse converted from screen to world).
        Vector3 targetPosition = ClampPosition(new Vector3(worldMousePosition.x, transform.position.y, worldMousePosition.z));
        transform.position = Vector3.Lerp(transform.position, targetPosition, movementSmoothness * Time.deltaTime);
    }

    // Set World Space boundaries for the Spaceship movement.
    private Vector3 ClampPosition(Vector3 positionCoordinates)
    {
        float clampedX = Mathf.Clamp(positionCoordinates.x, minX, maxX);
        float clampedZ = Mathf.Clamp(positionCoordinates.z, minZ, maxZ);
        
        return new Vector3(clampedX, positionCoordinates.y, clampedZ);
    }

    // Spaceship Weapons ------------------------------------------------------ //
    private void ShootWeapons()
    {
        if (shootAction.IsPressed() && !isShooting)
        {
            isShooting = true;
            Debug.Log("the Spaceship is shooting!");
            OnShooting?.Invoke();
        }
        else if (!shootAction.IsPressed() && isShooting)
        {
            isShooting = false;
            Debug.Log("The Spaceship stopped shooting!");
            OnStopShooting?.Invoke();
        }
    }
}
