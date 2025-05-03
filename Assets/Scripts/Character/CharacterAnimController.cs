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
    public void SetRuning(bool isRuning)
    {
        animator.SetBool("isRunning",isRuning);
    }
    public void SetJumping()
    {
        animator.SetTrigger("isJumping");
    }
    public void SetRolling()
    {
        animator.SetTrigger("isRolling");
    }
    public void SetAttack(int attackNum)
    {
        animator.SetTrigger("Attack"+attackNum);
    }
    public void SetFall(float AirSpeedY)
    {
        animator.SetFloat("isFalling", AirSpeedY);
    }
    public void SetHurt()
    {
        animator.SetTrigger("isHurting");
    }
    public void SetDeath()
    {
        animator.SetTrigger("isDeath");
    }

}
