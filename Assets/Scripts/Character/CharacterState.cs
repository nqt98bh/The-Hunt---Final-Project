using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{

    [SerializeField] private int maxHP = 100;
    private int currentHP;
    CharacterAnimController animator;
    CharacterMovement characterMovement;

    private void Awake()
    {
        animator = GetComponent<CharacterAnimController>();
        characterMovement = GetComponent<CharacterMovement>();
        currentHP = maxHP;

    }
    private void Update()
    {
        Dead();
    }

    public void TakeDamage(int damage)
    {
        if (characterMovement != null && characterMovement.isBlocking)
        {
          return;
        }
        currentHP -= damage;
        animator.SetTriggerHurt();

        Debug.Log("Current HP: " + currentHP);
    }
    void Dead()
    {
        if (currentHP <= 0)
        {
            currentHP = 0;
            animator.SetTriggerDeath();
        }
    }
}
