using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;

    private bool isWalking = false;

    private void Update()
    {
        Vector2 input = new Vector2(0, 0);

        // Capture input
        if (Input.GetKey(KeyCode.W))
        {
            input.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;
        }

        input = input.normalized;
        Vector3 moveDir = new Vector3(input.x, 0, input.y);

        // Update walking state
        isWalking = moveDir.magnitude > 0;

        // Move the player
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // Rotate the player to face the movement direction
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
