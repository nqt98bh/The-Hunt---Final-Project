using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSkill : ISkill
{
    float coolDown =2f;
    
    public float CoolDown => coolDown;
    float lastTimeUse =0f;

    public bool CanUse(BossAI boss, CharacterController player, float attackRange)
    {

        float distance = Vector3.Distance(player.transform.position, boss.transform.position);
        EnemyConfig bossConfig = boss.GetConfig();

        float hp = boss.GetCurrentHP();
        float maxHp = bossConfig.maxHealth;
        // health 2/3 -> 1/3: i.e. 0.33–0.66 fraction
        if ( hp >= (maxHp *2 / 3f) && distance < attackRange && Time.time - lastTimeUse > coolDown)
        { 
            Debug.Log("Can Punch");
            return true;
        }
        return false;
       

    }

    public bool Execute(BossAI boss, CharacterController player)
    {
        lastTimeUse = Time.time;
        boss.SetTriggerAnim("Attack1");
        return true;
        
    }
    public void PunchAttack(BossAI boss, CharacterController player)
    {
        Collider2D hit = Physics2D.OverlapBox(boss.PuchAttackPoint.position, boss.punchSize, 0, boss.GetConfig().playerLayer);
        if(hit != null)
        {
            if(hit.gameObject == player.gameObject)
            {
                player.TakeDamage(boss.GetConfig().damage);
            }
        }
    }

}
