using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private PlayerMovement playerMovement;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALKING, playerMovement.IsWalking());
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, playerMovement.IsWalking());
    }
}
