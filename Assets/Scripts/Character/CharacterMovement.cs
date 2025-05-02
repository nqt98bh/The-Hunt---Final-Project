using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] private float speed = 5f;
    private float horizontalInput;
    private bool facingRight =true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

       
    }
    private void FixedUpdate()
    {
        if (horizontalInput == 0) animator.SetBool("isRuning", false);
        else animator.SetBool("isRuning", true);
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        Flip(horizontalInput);
    }
    private void Flip(float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
  
}

