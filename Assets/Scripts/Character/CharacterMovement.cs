using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb;
    CharacterAnimController anim;
    CharacterInput input;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private bool facingRight =true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<CharacterInput>();
        anim = GetComponent<CharacterAnimController>();
    }


    void Update()
    {

       
    }
    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        rb.velocity = new Vector2(input.HorizontalInput * speed, rb.velocity.y);
        if (input.HorizontalInput < 0 && facingRight || input.HorizontalInput > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        anim.SetRuning(input.HorizontalInput!=0);
    }
   
  
}

