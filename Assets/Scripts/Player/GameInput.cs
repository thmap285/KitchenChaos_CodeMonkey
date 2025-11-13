using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        inputActions.Player.Interact.performed += ctx =>
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        };
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 input = inputActions.Player.Move.ReadValue<Vector2>();

        input = input.normalized;

        // Debug.Log(input);
        return input;
    }
}
