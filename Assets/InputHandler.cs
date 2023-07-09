using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float moveAmount;

    public Vector2 movement;

    PlayerInput inputActions;
    public PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInput();
            inputActions.Movement.Move.performed += inputActions => movement = inputActions.ReadValue<Vector2>();
        }

        inputActions.Enable();
    }

    public void TickInput(float delta)
    {
        HandleMovementInput(delta);
    }
    private void HandleMovementInput(float delta)
    {
        if(movement == Vector2.zero) { return; }
        playerManager.Move(movement.x, movement.y);
    }
}
