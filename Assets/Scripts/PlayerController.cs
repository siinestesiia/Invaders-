using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    
    InputAction moveAction;
    InputAction leftWeaponAttack;
    InputAction rightWeaponAttack;


    private Vector2 mousePosition;
    [Header ("Movement Boundaries of the Screen.")]
    [SerializeField] float minX = -28;
    [SerializeField] float maxX = 28;
    [SerializeField] float minZ = -15;
    [SerializeField] float maxZ = 14;

    private float cameraHeight;

    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        leftWeaponAttack = playerInput.actions["LeftWeaponAttack"];
        rightWeaponAttack = playerInput.actions["RightWeaponAttack"];

        cameraHeight = Camera.main.transform.position.y;
    }

    void Update()
    {
        SetPlayerPosition();
        WeaponAttack();
    }


    // Spaceship Movement ----------------------------------------------------
    private void SetPlayerPosition()
    {
        mousePosition = moveAction.ReadValue<Vector2>();
        
        Vector3 screenPoint = new Vector3(mousePosition.x, mousePosition.y, cameraHeight);
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(screenPoint);

        Vector3 newPosition = new Vector3(worldMousePosition.x, transform.position.y, worldMousePosition.z);
        transform.position = ClampPosition(newPosition);
    }

    // Set the screen boundaries for the Spaceship movement.
    private Vector3 ClampPosition(Vector3 positionCoordinates)
    {
        float clampedX = Mathf.Clamp(positionCoordinates.x, minX, maxX);
        float clampedZ = Mathf.Clamp(positionCoordinates.z, minZ, maxZ);
        
        return new Vector3(clampedX, positionCoordinates.y, clampedZ);
    }

    // Spaceship Weapons ------------------------------------------------------
    private void WeaponAttack()
    {
        if (leftWeaponAttack.IsPressed())
        {
            Debug.Log("Shooting left weapon!");
        }
        
        if (rightWeaponAttack.IsPressed())
        {
            Debug.Log("Shooting right weapon!");
        }
    }
}
