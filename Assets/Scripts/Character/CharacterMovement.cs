using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rollForce = 5f;

    Rigidbody2D rb2d;
    CharacterAnimController anim;
    CharacterInput input;

    [SerializeField] Sensor_Character wallSensor;
    [SerializeField] Sensor_Character GroundSensor;

    private bool isGrounded = false;
    private bool facingRight =true;
    private bool isRolling = false;
    private bool isSliding = false;

    private float delayToIlde = 0f;
    private float currentTimeRolling;
    private float rollDuration = 5f;
    private float timeSinceAttack;
    private int attackIndex = 0;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponent<CharacterInput>();
        anim = GetComponent<CharacterAnimController>();
        wallSensor = GetComponentInChildren<Sensor_Character>();
        GroundSensor = GetComponentInChildren<Sensor_Character>();
    }


    void Update()
    {
        anim.SetAirSpeedY(rb2d.velocity.y);

        CheckGround();
       
        Jump();
        Roll();
        Attack();
        Block();
    }
    private void FixedUpdate()
    {
       
        Movement();
    }

    void CheckGround()
    {
        if (!isGrounded && GroundSensor.State())
        {
            isGrounded = true;
            anim.SetBoolIsGrounded(isGrounded);
        }
        if (isGrounded && !GroundSensor.State())
        {
            isGrounded = false;
            anim.SetBoolIsGrounded(isGrounded);
        }
    }

    void Movement()
    {
        rb2d.velocity = new Vector2(input.HorizontalInput * speed, rb2d.velocity.y);
        
        if (input.HorizontalInput < 0 && facingRight || input.HorizontalInput > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

        }
        if (Mathf.Abs(input.HorizontalInput) > Mathf.Epsilon)
        {
            delayToIlde = 0.001f;
            anim.SetRuning(true);
        }
        else 
        {
            delayToIlde -= Time.fixedDeltaTime;
            if (delayToIlde <= 0)
            {
                anim.SetRuning(false);
            }
        }
    }
    void Jump()
    {
        if(isGrounded == true && isRolling == false && input.JumpPressed)
        {
            anim.SetTriggerJumping();
            isGrounded = false;
            anim.SetBoolIsGrounded(isGrounded);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            GroundSensor.Disable(0.2f);

        }
    }
    void Roll()
    {
        if(input.RollPressed && !isRolling && !isSliding)
        {
            rb2d.velocity = new Vector2(transform.forward.x*rollForce, rb2d.velocity.y);
            isRolling = true;
            anim.SetRolling();
            
        }
        currentTimeRolling += Time.deltaTime;
        if (currentTimeRolling > rollDuration)
        {
            isRolling = false;
            Debug.Log("isRolling: " + isRolling);
        }
    }
    
    void Attack()
    {
        timeSinceAttack += Time.deltaTime;
        if(input.AttackPressed && !isRolling && !isSliding && timeSinceAttack > 0.25f)
        {
            attackIndex++;
            if(attackIndex >3)  attackIndex = 1; 
            if(timeSinceAttack >1.0f) attackIndex = 1;
            anim.SetAttack(attackIndex);
            timeSinceAttack = 0;
        }
    }
    void Block()
    {
        if(input.BlockPressed && !isRolling && !isSliding)
        {
            anim.SetTriggerBlock();
            anim.SetBoolIdleBlock(true);
        }
        else if (input.UnBlockPressed)
        {
            anim.SetBoolIdleBlock(false);
        }
    }
}

