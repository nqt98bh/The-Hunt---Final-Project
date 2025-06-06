using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puch : ISkill
{
    float coolDown;
    public float CoolDown => coolDown;
    float lastTimeUse =0f;
    

    public bool CanUse(BossAI boss, CharacterController player)
    {
        EnemyConfig config = new EnemyConfig();
        config = boss.GetConfig();
        coolDown = config.attackCooldown;
        float distance = Vector3.Distance(player.transform.position,boss.transform.position);
        if(distance < config.attackRange && Time.time - lastTimeUse > coolDown)
        {
            return true;
        }
        return false;

    }

    public void Excute(BossAI boss, CharacterController player)
    {
        lastTimeUse = Time.time;
        boss.SetTriggerAnim("isAttacking");
        
    }
}
