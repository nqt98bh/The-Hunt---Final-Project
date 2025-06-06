using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BossAI : EnemyAI
{
    
    Animator anim;
    protected override bool DetectionPlayer()
    {
        float distance = Vector3.Distance(player.position,transform.position);
        
        if(distance < config.detectionRange)
        {
            return true;
        }
        return false;
    }

    protected override float GetDirection(bool isChasing)
    {
        return Mathf.Sign(player.position.x - transform.position.x);
    }

    public void SetTriggerAnim(string animName)
    {
        anim.SetTrigger(animName);
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,config.attackRange);
        Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
        Gizmos.DrawWireSphere(transform.position, config.detectionRange);

    }

}
