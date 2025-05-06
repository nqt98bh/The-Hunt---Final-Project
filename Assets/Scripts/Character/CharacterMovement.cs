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
    CapsuleCollider2D capsuleCollider;
    CharacterAnimController anim;
    CharacterInput input;


    [SerializeField] Sensor_Character wallSensor1;
    [SerializeField] Sensor_Character wallSensor2;
    [SerializeField] Sensor_Character GroundSensor;

    private bool isGrounded = false;
    private bool facingRight =true;
    private bool isRolling = false;
    private bool isSliding = false;
    private bool isJumping = false;

    private float delayToIlde = 0f;
    private float currentTimeRolling;
    [SerializeField] private float rollDuration = 2f;
    private float timeSinceAttack;
    private int attackIndex = 0;
    [SerializeField] public float dragForce = 30f;



    [SerializeField] private Vector2 colliderSize = new Vector2(0.7f, 0.65f);
    [SerializeField] private Vector2 colliderOffset = new Vector2(0f, 0.45f);
    private Vector2 colliderSizeDefault /*=  new Vector2(0.7f, 1.29f)*/;
    private Vector2 colliderOffsetDefault /*= new Vector2(0, 0.7f)*/;
    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponent<CharacterInput>();
        anim = GetComponent<CharacterAnimController>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
       
    }

    private void Start()
    {
        colliderSizeDefault = capsuleCollider.size;
        colliderOffsetDefault = capsuleCollider.offset;

    }
    void Update()
    {
        anim.SetAirSpeedY(rb2d.velocity.y);
        Jump();
        Roll();
        Attack();
        Block();
        WallSliding();
        CheckGround();
    }
    private void FixedUpdate()
    {
       
        Movement();
        if (isGrounded == false && rb2d.velocity.y > 0.01f && isJumping == false)
        {
            rb2d.AddForce(Vector2.down * dragForce);
        }
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
            isJumping = true;
            anim.SetTriggerJumping();
            isGrounded = false;
            anim.SetBoolIsGrounded(isGrounded);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            GroundSensor.Disable(0.2f);

        }
        if(isGrounded == true)
        {
            isJumping = false;
        }
    }

    //Roll
    void Roll()
    {
        if(input.RollPressed && isRolling == false && !isSliding)
        {
            rb2d.velocity = new Vector2(transform.forward.x*rollForce, rb2d.velocity.y);
            

            isRolling = true;
            anim.SetRolling();
            
        }
        currentTimeRolling += Time.deltaTime;
        if (currentTimeRolling > rollDuration)
        {
            
            isRolling = false;
            currentTimeRolling = 0;


        }
    }
    public void OnableRollCollision()
    {
        capsuleCollider.offset = colliderOffset;
        capsuleCollider.size = colliderSize;
    }
    public void DisableRollCollision()
    {
        capsuleCollider.offset = colliderOffsetDefault;
        capsuleCollider.size = colliderSizeDefault;
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
    void WallSliding()
    {
        
        isSliding = (wallSensor1.State() && wallSensor1.State());
        anim.SetBoolSliding(isSliding);
    }
}

