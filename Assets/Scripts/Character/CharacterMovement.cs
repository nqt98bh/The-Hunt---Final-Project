using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{

     private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rollForce = 5f;

    Rigidbody2D rb2d;
    CapsuleCollider2D capsuleCollider;
    CharacterAnimController anim;
    CharacterInput input;
    CharacterController characterController;
    [SerializeField] CapsuleCollider2D playerCollider;


    [SerializeField] Wall_Sensor wallSensor1;
    [SerializeField] Wall_Sensor wallSensor2;
    [SerializeField] Ground_Sensor groundSensor;
 

    private bool isGrounded = false;
    private bool facingRight =true;
    private bool isRolling = false;
    private bool isSliding = false;
    private bool isJumping = false;
    public bool isBlocking { get; private set; } = false;
    public bool isFreezing { get; private set; } = false;

    private float delayToIlde = 0f;
    private float currentTimeRolling;
    [SerializeField] private float rollDuration = 2f;
    private int jumpCount = 0;
    private float timeSinceAttack;
    private int attackIndex = 0;
    [SerializeField] float dragForce = 30f;
    [SerializeField] float disableLayerCollision = 0.2f;

    [SerializeField] private Vector2 colliderSize = new Vector2(0.7f, 0.65f);
    [SerializeField] private Vector2 colliderOffset = new Vector2(0f, 0.45f);
    private Vector2 colliderSizeDefault /*=  new Vector2(0.7f, 1.29f)*/;
    private Vector2 colliderOffsetDefault /*= new Vector2(0, 0.7f)*/;
    Coroutine c_waitForEnableLayerCollision;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponent<CharacterInput>();
        anim = GetComponent<CharacterAnimController>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        characterController = GetComponent<CharacterController>();
       
    }

    private void Start()
    {
        colliderSizeDefault = capsuleCollider.size;
        colliderOffsetDefault = capsuleCollider.offset;

    }
    void Update()
    {
        if (GameManager.Instance.IsPauseGame()) return;
        if (GameManager.Instance.isGameOver || characterController.IsFrozen() == true )
        {
            return;
        }
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
        if (GameManager.Instance.IsPauseGame()) return;
        if (GameManager.Instance.isGameOver) return;
        Movement();
        if (isGrounded == false && rb2d.velocity.y > 0.01f && isJumping == false)
        {
            rb2d.AddForce(Vector2.down * dragForce);
        }
    }

    void CheckGround()
    {
        if (!isGrounded && groundSensor.State())
        {
            isGrounded = true;
            anim.SetBoolIsGrounded(isGrounded);
        }
       else if (isGrounded && !groundSensor.State())
        {
            isGrounded = false;
            anim.SetBoolIsGrounded(isGrounded);
        
        }
      
    }

    void Movement()
    {
        //Debug.Log($"Movement(): IsFrozen={characterController.IsFrozen()}, speed={speed}");
        if (isBlocking || characterController.IsFrozen() ==true)
        {
            
            speed = 0;
        }
        else
        {
           speed =  characterController.maxSpeed;
        }
        rb2d.velocity = new Vector2(input.HorizontalInput * speed, rb2d.velocity.y);

        if ((input.HorizontalInput < 0 && facingRight || input.HorizontalInput > 0 && !facingRight) && characterController.IsFrozen() ==false)
        {
            facingRight = !facingRight;
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

        }
        if (Mathf.Abs(input.HorizontalInput) > Mathf.Epsilon && !isBlocking && characterController.IsFrozen() ==false)
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
    public void PlaySoundMovement()
    {
        GameManager.Instance.PlaySoundFX(SoundType.playerRun);
    }
    void Jump()
    {
        
        if(jumpCount <2 || isGrounded == true)
        {
            if ( isRolling == false && input.JumpPressed)
            {
                isJumping = true;
                jumpCount++;
                anim.SetTriggerJumping();
                GameManager.Instance.PlaySoundFX(SoundType.playerJump);
                isGrounded = false;
                anim.SetBoolIsGrounded(isGrounded);
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                groundSensor.Disable(0.2f);
                Debug.Log("Jumpcount: " + jumpCount);

            }
        }
            
        
     
        if(isGrounded == true)
        {
            isJumping = false;
            jumpCount = 0;
        }
    }

    //Roll
    void Roll()
    {
        if(input.RollPressed && isRolling == false && !isSliding)
        {
            if (groundSensor.CollisionDetect() != null)
            {
                if (c_waitForEnableLayerCollision != null)
                {
                    StopCoroutine(c_waitForEnableLayerCollision);
                    c_waitForEnableLayerCollision = null;
                }
                
                c_waitForEnableLayerCollision = StartCoroutine(EnablePlatformCollision());
                //StartCoroutine(EnablePlatformCollision(OneWayPlatform));
            }
            else
            {
                rb2d.velocity = new Vector2(transform.forward.x * rollForce, rb2d.velocity.y);
                GameManager.Instance.PlaySoundFX(SoundType.playerRoll);
                isRolling = true;
                anim.SetRolling();
            }
            
        }
        currentTimeRolling += Time.deltaTime;
        if (currentTimeRolling > rollDuration)
        {
            isRolling = false;
            currentTimeRolling = 0;
        }
    }
    IEnumerator EnablePlatformCollision()
    {
        TilemapCollider2D platformColiider = groundSensor.CollisionDetect().GetComponent<TilemapCollider2D>();
        Physics2D.IgnoreCollision(playerCollider,platformColiider);
        yield return new WaitForSeconds(disableLayerCollision);
        Physics2D.IgnoreCollision(playerCollider,platformColiider,false);

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
        if (input.AttackPressed && !isRolling && !isSliding && timeSinceAttack > 0.25f)
        {
            attackIndex++;
            if (attackIndex > 3) attackIndex = 1;
            if (timeSinceAttack > 1.0f) attackIndex = 1;
            anim.SetAttack(attackIndex);
            timeSinceAttack = 0;
            if (attackIndex == 1)
            {
                GameManager.Instance.PlaySoundFX(SoundType.playerAttack1);

            }
            else if (attackIndex == 2)
            {
                GameManager.Instance.PlaySoundFX(SoundType.playerAttack2);

            }
            else
            {
                GameManager.Instance.PlaySoundFX(SoundType.playerAttack3);

            }
        }
    }
 
    void Block()
    {
        if(input.BlockPressed && !isRolling && !isSliding)
        {
            isBlocking = true;
            anim.SetTriggerBlock();
            anim.SetBoolIdleBlock(true);
            GameManager.Instance.PlaySoundFX(SoundType.playerBlock);
        }
        else if (input.UnBlockPressed)
        {
            isBlocking = false ;
            anim.SetBoolIdleBlock(false);
        }
        
    }
    void WallSliding()
    {

        isSliding = (!isGrounded&& wallSensor1.State() && wallSensor2.State());
        anim.SetBoolSliding(isSliding);
        if (isSliding)
        {
            rb2d.gravityScale = 0.5f;
            //GameManager.Instance.PlaySoundFX(SoundType.playerWallSliding);
            //Debug.Log("Play Wall sliding Sound");

        }
        else 
        {
            rb2d.gravityScale = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            characterController.TakeDamage(characterController.maxHP);
        }
    }
}

