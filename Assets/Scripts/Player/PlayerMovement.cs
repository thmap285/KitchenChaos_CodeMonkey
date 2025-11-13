using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;

    private void Update()
    {
        Vector2 input = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(input.x, 0, input.y);
        
        bool canMove = !Physics.CapsuleCast(transform.position, 
                                              transform.position + Vector3.up * 2f, 
                                              0.7f, 
                                              moveDir, 
                                              moveSpeed * Time.deltaTime);

        if(!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, 
                                              transform.position + Vector3.up * 2f, 
                                              0.7f, 
                                              moveDirX, 
                                              moveSpeed * Time.deltaTime);

            if(canMove)
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

                if(canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    moveDir = Vector3.zero;
                }
            }
        }
        
        if(canMove)
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
