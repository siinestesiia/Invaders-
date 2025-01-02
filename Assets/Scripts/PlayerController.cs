using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Player Input
    PlayerInput playerInput;

    // Input actions
    InputAction moveAction;
    private InputAction leftAttackAction;
    private InputAction rightAttackAction;

    // Movement variable stores x and y values.
    Vector2 mousePosition;

    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // The name in "" must match the action name.
        moveAction = playerInput.actions["move"];
        // leftAttackAction = playerInput.actions["LeftWeaponAttack"];       
        // rightAttackAction = playerInput.actions["RightWeaponAttack"];       
    }

    void Update()
    {
        float cameraHeight = Camera.main.transform.position.y;
    
        // Handling Mouse Movement
        mousePosition = moveAction.ReadValue<Vector2>();
        Vector3 screenPoint = new Vector3(mousePosition.x, mousePosition.y, cameraHeight);
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(screenPoint);

        Vector3 newPosition = new Vector3(worldMousePosition.x, transform.position.y, worldMousePosition.z);

        Debug.Log($"World Mouse Position: {worldMousePosition} | Transformed Position: {newPosition}");
        transform.position = newPosition;

        // Debug.Log($"Left Attack Triggered: {leftAttackAction.triggered}");
        // Debug.Log($"Right Attack Triggered: {rightAttackAction.triggered}");
    }
}
