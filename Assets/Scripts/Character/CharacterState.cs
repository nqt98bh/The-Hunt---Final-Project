using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{

    [SerializeField] private int maxHP = 100;
    [SerializeField] float attackRadius;
    private int playerDamage = 10;
    private int currentHP;
    CharacterAnimController animator;
    CharacterMovement characterMovement;


    public Transform attackPoint;

    public LayerMask enemyLayer;
    private void Awake()
    {
        animator = GetComponent<CharacterAnimController>();
        characterMovement = GetComponent<CharacterMovement>();
        currentHP = maxHP;

    }
    private void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (characterMovement != null && characterMovement.isBlocking)
        {
          return;
        }
        currentHP -= damage;
        animator.SetTriggerHurt();
        Dead();
        Debug.Log("Current HP: " + currentHP);
    }
    public void AttackEnemy()
    {
        Collider2D colliderInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);
        if (colliderInfo != null)
        {
            Debug.Log(colliderInfo.transform.name);
            EnemyAI target = colliderInfo.GetComponent<EnemyAI>();
            if (target != null)
            {
                target.TakeDamage(playerDamage);
            }
        }
    }
    void Dead()
    {
        if (currentHP <= 0)
        {
            currentHP = 0;
            animator.SetTriggerDeath();
            GameManager.Instance.isGameOver = true;
        }
        
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
