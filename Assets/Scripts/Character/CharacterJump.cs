using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask ground;
    [SerializeField] private bool isGrounded = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, distance, ground);
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("isJumping");
            isGrounded = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position,distance);
    }
}
