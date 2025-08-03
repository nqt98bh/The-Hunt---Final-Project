using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : IAttackable
{
   

    public void Attack(Transform attackPoint, float radius, CharacterController target, EnemyConfig config)
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, config.playerLayer);
        if(hit != null)
        {
            if(hit.gameObject == target.gameObject)
            {
                target.TakeDamage(config.damage);
            }
        }
    }
}
