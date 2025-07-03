using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour ,IDataPersistence
{
    //public static CharacterController Instance;

    public static Action OnHealthChanged;
    public int maxHP = 100;
    private int currentHP;
    public int CurrentHP => currentHP;
    public int playerDamage = 10;
    public float maxSpeed = 5f;
    public int defend = 5;

    [SerializeField] float attackRadius;
    CharacterAnimController animator;
    CharacterMovement characterMovement;
    
    
    public Transform attackPoint;

    public LayerMask enemyLayer;

    private bool isFrozen = false;
    public bool IsFrozen() => isFrozen;

    
    private void Awake()
    {
        animator = GetComponent<CharacterAnimController>();
        characterMovement = GetComponent<CharacterMovement>();

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
            GameManager.Instance.PlaySoundFX(SoundType.blockDamage);
            return;
        }
        
        currentHP -= damage;
        if (isFrozen == false)
        {
            animator.SetTriggerHurt();

        }
        OnHealthChanged?.Invoke();
        if (currentHP <= 0)
        {

            Dead();

            return;
        }
        GameManager.Instance.PlaySoundFX(SoundType.playerHit);
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
        
            //currentHP = 0;
            animator.SetTriggerDeath();
            characterMovement.enabled = false;
            
            //this.gameObject.SetActive(false);
            GameManager.Instance.GameOver();
            GameManager.Instance.PlaySoundFX(SoundType.playerDeath);
        
        
    }
    public void ResetHealth()
    {
        currentHP = maxHP;
        OnHealthChanged?.Invoke();
    }
    public void SetMaxHP(int maxHP)
    {
        this.maxHP = maxHP;
        currentHP = maxHP;
        OnHealthChanged?.Invoke();
    }
 
    public void Healing(int amount)
    {
        currentHP += amount;
        OnHealthChanged?.Invoke();
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
 

    public void LoadData(GameData data)
    {
        //if(data.characterData.playerPosition == null)
        //{

        //    this.transform.position = new Vector3(-3f, -4f, 0);
        //    return;
        //}
        this.currentHP = data.characterData.currentHP;
        this.transform.position = data.characterData.playerPosition;
        this.maxHP = data.characterData.maxHP;

    }

    public void SaveData(ref GameData data)
    {
        data.characterData.playerPosition = this.transform.position;
        data.characterData.currentHP = currentHP;
        data.characterData.playerDamage = playerDamage;
        
    }

    
}
