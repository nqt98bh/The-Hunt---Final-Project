using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class EnemyAI : MonoBehaviour
{
    [Header("Config & Target")]
    [SerializeField] protected EnemyConfig config;
    [SerializeField] protected CharacterController characterController;

    
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected Transform player;

    [SerializeField] protected float attackRadius = 1f;

    [SerializeField] protected int currentHP;
    float nextAttackTime = 0f;
    protected bool facingRight =true;
    protected IMoveable movement;
    protected IAttackable attack;
    protected Rigidbody2D rb;
    protected Animator animator;


    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHP = config.maxHealth;
        movement = StrategyFactory.CreateMovement(config.movementType);
        attack = StrategyFactory.CreateAttack(config.attackType, config);
        
    }

    protected virtual void Update()
    {
       bool isChasing = DetectionPlayer();
        if (isChasing)
        {
            AttackPlayer();
        }
        EnemyMovement();

    }

   
    protected virtual void EnemyMovement()
    {
       
        bool isChasing = DetectionPlayer();
        float speed = isChasing ? config.chaseSpeed : config.moveSpeed;
        float moveDirection = GetDirection(isChasing);

        if (Mathf.Abs(player.transform.position.x-transform.position.x)<config.attackRange) 
        {
            moveDirection = 0;
            animator.SetFloat("Moving_ID", 0);
        }
        else { animator.SetFloat("Moving_ID", DetectionPlayer() ? 1f : 0f); }

        movement.Move(rb, moveDirection, speed);
        DoFlip(moveDirection);

    }

   
    protected abstract bool DetectionPlayer();
    protected abstract float GetDirection(bool isChasing);

    protected void FacePlayer()
    {
        bool facePlayer = player.position.x >= transform.position.x;
        if (facePlayer != facingRight)
        {
            Flip();
        }
    }
    void DoFlip(float dir)
    {
        if (dir > 0f && !facingRight)
        {
            Flip();
        }
        else if (dir < 0f && facingRight)
        {
            Flip();
        }
    }
    protected void Flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    protected virtual void AttackPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if((distance < config.attackRange) && Time.time > nextAttackTime)
        {
            animator.SetTrigger("isAttacking");
            nextAttackTime = Time.time + config.attackCooldown;
            
        }
    }
    public void OnAttackAnimationHit()
    {
        attack.Attack(attackPoint, attackRadius, characterController, config);
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        animator.SetTrigger("isHitted");
        GameManager.Instance.PlaySoundFX(SoundType.enemyHit);
        if (currentHP <= 0)
        {
            currentHP = 0;
            animator.SetTrigger("isDeaded");
            Destroy(gameObject,0.5f);
        }
    }
   public EnemyConfig GetConfig()
    {
        return config;
    }
   public int GetCurrentHP()
    {
        return currentHP;
    }
    protected virtual void OnDrawGizmosSelected()
    {

    }
}
