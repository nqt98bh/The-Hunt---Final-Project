using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public static CharacterState Instance;

    public static Action<float> OnHealthChanged;
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
       if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
        animator = GetComponent<CharacterAnimController>();
        characterMovement = GetComponent<CharacterMovement>();
        currentHP = maxHP;

    }


    public void TakeDamage(int damage)
    {
        if (characterMovement != null && characterMovement.isBlocking)
        {
          return;
        }
        currentHP -= damage;
        
        OnHealthChanged?.Invoke((float)currentHP/maxHP);
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
    public void ResetHealth()
    {
        currentHP = maxHP;
        OnHealthChanged?.Invoke((float)currentHP/maxHP);
    }
    public void SetMaxHP(int maxHP)
    {
        this.maxHP = maxHP;
        currentHP = maxHP;
        OnHealthChanged?.Invoke((float)currentHP/maxHP);
    }
    public float GetCurrentHP()
    {
        return (float)currentHP/maxHP;
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
