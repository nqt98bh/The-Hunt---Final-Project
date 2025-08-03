using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class CharacterAnimController : MonoBehaviour
{
     Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetBoolIsGrounded(bool isGrounded)
    {
        animator.SetBool("isGrounded",isGrounded);
    }
    public void SetRuning(bool isRuning)
    {
        animator.SetBool("isRunning",isRuning);
    }
    public void SetTriggerJumping()
    {
        //animator.SetTrigger("isJumping");
        animator.Play("Jump");
    }
    public void SetRolling()
    {
        animator.SetTrigger("isRolling");
    }
    public void SetAttack(int attackNum)
    {
        animator.SetTrigger("Attack"+attackNum);
    }
    public void SetAirSpeedY(float AirSpeedY)
    {
        animator.SetFloat("AirSpeedY", AirSpeedY);
    }
    public void SetTriggerHurt()
    {
        animator.SetTrigger("isHurting");
    }
    public void SetTriggerDeath()
    {
        animator.SetTrigger("isDeath");
    }
    public void SetTriggerBlock()
    {
        animator.SetTrigger("Block");
    }
    public void SetBoolIdleBlock(bool blockState)
    {
        animator.SetBool("IdleBlock", blockState);
    }
    public void SetBoolSliding(bool isSliding)
    {
        animator.SetBool("isSliding", isSliding);
    }
    public void SetTriggerFrozen(string name)
    {
        animator.SetTrigger(name);
    }
    public void ClearTrigger(string name)
    {
        animator.ResetTrigger(name);
        
    }
   
}
