using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour ,IDataPersistence
{
    //public static CharacterController Instance;

    public static Action<float> OnHealthChanged;
    public int maxHP = 100;
    public int playerDamage = 10;
    public float maxSpeed = 5f;

    [SerializeField] float attackRadius;
    private int currentHP;
    CharacterAnimController animator;
    CharacterMovement characterMovement;
    
    
    public Transform attackPoint;
    [SerializeField] private Transform SavePoint;

    public LayerMask enemyLayer;

    private bool isFrozen = false;
    public bool IsFrozen() => isFrozen;
    private void Awake()
    {
        animator = GetComponent<CharacterAnimController>();
        characterMovement = GetComponent<CharacterMovement>();
        currentHP = maxHP;

    }

    public void EnterFrozen()
    {
        if(isFrozen) return;
        isFrozen = true;
        animator.SetTriggerFrozen("EnterFrozen");
    }

    public void OnHitBossFreeze(int damage)
    {
        if(!isFrozen) return;    
        animator.SetTriggerFrozen("Frozen_Destroy");
        isFrozen = false;
        TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        if (characterMovement != null && characterMovement.isBlocking)
        {
            Debug.Log("IsBlocking");
            GameManager.Instance.PlaySoundFX(SoundType.collideWithAttack);
            return;
        }
        currentHP -= damage;
        
        OnHealthChanged?.Invoke((float)currentHP/maxHP);
        animator.SetTriggerHurt();
        GameManager.Instance.PlaySoundFX(SoundType.playerHit);
        Dead();
        Debug.Log("Current HP: " + currentHP);
    }
    public void AttackEnemy()
    {
        Collider2D colliderInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);
        if (colliderInfo != null)
        {
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
            characterMovement.enabled = false;
            this.gameObject.SetActive(false);
            GameManager.Instance.GameFinished();
            GameManager.Instance.PlaySoundFX(SoundType.playerDeath);
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
    public void RestartGame()
    {
        ResetHealth();
        transform.position = SavePoint.position;
        characterMovement.enabled = true;
        this.gameObject.SetActive(true);
        
       
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.lastCheckPoint;
    }

    public void SaveData(ref GameData data)
    {
        //data.lastCheckPoint = this.transform.position;
    }
}
