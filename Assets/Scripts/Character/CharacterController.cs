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
    private int currentHP;
    public int playerDamage = 10;
    public float maxSpeed = 5f;
    public int defend = 5;

    [SerializeField] float attackRadius;
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
        animator.ClearTrigger("Frozen_Destroy");

        animator.SetTriggerFrozen("EnterFrozen");
        Debug.Log("Enter Frozen State");
    }

    public void OnHitBossFreeze(int damage)
    {
        if(!isFrozen) return;
        //animator.ClearTrigger("EnterFrozen");
        animator.SetTriggerFrozen("Frozen_Destroy");
        TakeDamage(damage);

        isFrozen = false;
        Debug.Log("Frozen_Destroy");
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
        if (isFrozen == false)
        {
            animator.SetTriggerHurt();

        }
        OnHealthChanged?.Invoke((float)currentHP/maxHP);
        GameManager.Instance.PlaySoundFX(SoundType.playerHit);
        Dead();
        Debug.Log("Current HP: " + currentHP);
    }
    public void AttackEnemy() //Attach on Attack Animation of Character
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
        return (float)currentHP / maxHP;
    }
    public void Healing(int amount)
    {
        currentHP += amount;
        OnHealthChanged?.Invoke(currentHP);
    }
    
    public void DamageUp(int amount)
    {
        playerDamage *= amount;
    }
    public void SpeedUp(float speed)
    {
        this.maxSpeed += speed;
    }
    public void DefendUp(int amount)
    {
        this.defend += amount;
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
