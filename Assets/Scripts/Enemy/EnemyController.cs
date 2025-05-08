using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] EnemyConfig config;

    [SerializeField] private float moveSpeed = 5f;


    private bool facingRight;
    private bool isChasing = false;

    public LayerMask groundLayer;
    public LayerMask playerLayer;

    public Transform GroundCheck;
    public Transform Player;
    public Transform frontCheck;

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSpeed = config.moveSpeed;
        facingRight = transform.position.x > 0;
    }
    private void Update()
    {


        bool isAttack = InAttackRange();
            animator.SetBool("isAttacking", isAttack);
        

        if (DetectionPlayer() && !isAttack)
        {
            isChasing = true;
            moveSpeed = config.chaseSpeed;
            animator.SetBool("isChasing", isChasing);
            Debug.Log("Chase player");
        }
        
        else 
        {
            isChasing = false;
            moveSpeed = config.moveSpeed;
            animator.SetBool("isChasing", isChasing);
        }
      
        if (!isChasing && (!OutOfGround() || ObstacleAhead()))
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
       
        if (isChasing)
        {
            FacePlayer();
            moveDirection = (Player.position - transform.position).normalized.x;
            
        }
        else
        {
            moveDirection = facingRight ? 1f : -1f;
        }
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

    }

    bool OutOfGround()
    {
        return Physics2D.Raycast(GroundCheck.position,Vector2.down,0.2f,groundLayer);

    }
    bool ObstacleAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(frontCheck.position, facingRight ? Vector2.right : Vector2.left, 0.5f, groundLayer);
        return hit.collider != null;
    }
    bool DetectionPlayer()
    {
        if(Player == null) return false;
        Vector2 direction = (Player.transform.position - transform.position).normalized; //Đảm bảo raycast luôn hướng về player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, config.detectionRange, playerLayer|groundLayer);
        return hit.collider != null && hit.collider.CompareTag("Player");
    }
    void FacePlayer()
    {
        bool facePlayer = Player.transform.position.x > transform.position.x;
        if (facePlayer != facingRight)
        {
            Flip();
        }
    }
    bool InAttackRange()
    {
        float distance = Vector2.Distance(transform.position, Player.position);
        if(distance<config.attackRange)
        {
            return true;
           
        }
        return false;

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
        Gizmos.color = Color.red;
        Gizmos.DrawRay(GroundCheck.position, Vector2.down * 0.2f);
        Gizmos.DrawRay(frontCheck.position, Vector2.right * 0.5f);
        Gizmos.DrawWireSphere(transform.position, config.detectionRange);
        if (isChasing && Player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, Player.position);
        }
    }
}
