using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private GameInput gameInput;

    private event EventHandler OnInteractAction;

    private bool isWalking = false;
    private Vector3 lastInteractDir;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        Vector2 input = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(input.x, 0, input.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if (Physics.Raycast(transform.position, moveDir, out RaycastHit hitInfo, 2f, counterLayerMask))
        {
            if (hitInfo.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    clearCounter.Interact();
                }
            }
        }
    }

    private void Update()
    {
        HandleInteraction();
        HandleMovement();
    }

    private void HandleInteraction()
    {
        // Vector2 input = gameInput.GetMovementVectorNormalized();
        // Vector3 moveDir = new Vector3(input.x, 0, input.y);

        // if (moveDir != Vector3.zero)
        // {
        //     lastInteractDir = moveDir;
        // }

        // if (Physics.Raycast(transform.position, moveDir, out RaycastHit hitInfo, 2f, counterLayerMask))
        // {
        //     if (hitInfo.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
        //     {
        //         if (Input.GetKeyDown(KeyCode.E))
        //         {
        //             clearCounter.Interact();
        //         }
        //     }
        // }
    }

    private void HandleMovement()
    {
        Vector2 input = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(input.x, 0, input.y);

        bool canMove = !Physics.CapsuleCast(transform.position,
                                              transform.position + Vector3.up * 2f,
                                              0.7f,
                                              moveDir,
                                              moveSpeed * Time.deltaTime);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position,
                                              transform.position + Vector3.up * 2f,
                                              0.7f,
                                              moveDirX,
                                              moveSpeed * Time.deltaTime);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position,
                                                  transform.position + Vector3.up * 2f,
                                                  0.7f,
                                                  moveDirZ,
                                                  moveSpeed * Time.deltaTime);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    moveDir = Vector3.zero;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        // Update walking state
        isWalking = moveDir.magnitude > 0;

        // Rotate the player to face the movement direction
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
