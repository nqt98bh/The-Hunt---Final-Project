using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{

    [SerializeField] private EnemyConfig config;
    [SerializeField] private CharacterState characterState;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private float attackCoolDown = 2f;
    private int currentHP;
    private float nextAttackTime = 0f;
    [SerializeField] private bool facingRight;
    private bool isChasing = false;
    private bool isAttacking = false;

    public LayerMask groundLayer;
    public LayerMask playerLayer;

    public Transform groundCheck;
    public Transform frontCheck;
    public Transform attackPoint;
    public Transform player;


    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSpeed = config.moveSpeed;
        currentHP = config.maxHealth;
        
    }
    private void Update()
    {
        Debug.Log("faceing right:" + facingRight);

        bool isAttack = InAttackRange();


        nextAttackTime += Time.deltaTime;

        if (DetectionPlayer()  )
        {
            if (isAttack )
            {
                
                if(nextAttackTime >= attackCoolDown)
                {
                    animator.SetTrigger("isAttacking");
                    nextAttackTime = 0f;
                }
               
            }

            isChasing = true;
            moveSpeed = config.chaseSpeed;
            animator.SetFloat("Moving_ID", 1);
            Debug.Log("Chase player");
            Debug.Log("isChasing:" + isChasing);

        }

        else 
        {
            animator.SetBool("isAttacking", false);
            isChasing = false;
           
            moveSpeed = config.moveSpeed;
            animator.SetFloat("Moving_ID", 0);

        }
      
        if ( !InGrounded() || ObstacleAhead())
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
       
        EnemyMovement();
    }
    private void EnemyMovement()
    {
        float moveDirection;
       
        if (isChasing && InGrounded() && !ObstacleAhead())
        {
            FacePlayer();

            moveDirection = (player.position - transform.position).normalized.x;
            
        }
        else
        {
            moveDirection = facingRight ? 1f : -1f;
        }
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

    }

    bool InGrounded()
    {
        return Physics2D.Raycast(groundCheck.position,Vector2.down,0.1f,groundLayer);
        

    }
    bool ObstacleAhead() //hỏi TA tại sao raycast không quay
    {
        RaycastHit2D hit = Physics2D.Raycast(frontCheck.position,facingRight ? Vector2.right : Vector2.left, 0.5f, groundLayer);
        return hit.collider != null;
    }
    bool DetectionPlayer()
    {
        if(player == null) return false;
        Vector2 target = player.position + Vector3.up * 0.5f;
        Vector2 direction = new Vector2( (target.x  - transform.position.x),0f).normalized; //Đảm bảo raycast luôn hướng về player

        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, direction, config.detectionRange, playerLayer|groundLayer);
        Debug.DrawRay(transform.position, direction * config.detectionRange, Color.green);
        bool seePlayer = hitPlayer.collider != null && hitPlayer.transform == player;
        if (seePlayer )
        {
            return true;
        }
        return false;
      
    }
    void FacePlayer()
    {
        bool facePlayer = player.transform.position.x > transform.position.x;
        if (facePlayer != facingRight)
        {
            Flip();
        }
    }
    bool InAttackRange()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if(distance<config.attackRange)
        {
            return true;
           
        }
        return false;

    }
    public void Attack()
    {
        Collider2D colliderInfor = Physics2D.OverlapCircle(attackPoint.position,attackRadius,playerLayer);
        if(colliderInfor != null)
        {
            if(colliderInfor.gameObject == characterState.gameObject)
            {
                characterState.TakeDamage(config.damage);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        animator.SetTrigger("isHitted");
        if(currentHP <= 0)
        {
            currentHP = 0;
            animator.SetTrigger("isDeaded");
            Destroy(gameObject);
        }
    }
    void Flip()
    {
        
        facingRight = !facingRight;
       
        Vector2 scale =  transform.localScale;
        scale.x *=  -1;
        transform.localScale = scale;

        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(groundCheck.position, Vector2.down * 0.2f);
        Gizmos.DrawRay(frontCheck.position,(facingRight ? Vector2.right : Vector2.left) *0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.detectionRange);
        if (isChasing && player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, player.position);
        }
        Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
    }
}
