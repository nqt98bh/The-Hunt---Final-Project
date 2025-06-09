using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoundSkill : ISkill
{
    float coolDown = 2f;
    public float CoolDown => coolDown;
    float lastTimeUse = 0f;
    public bool CanUse(BossAI boss, CharacterController player, float attackRange)
    {
        float distance = Vector2.Distance(player.transform.position, boss.transform.position);
        EnemyConfig bossConfig = boss.GetConfig();

        if (boss.GetCurrentHP() < (bossConfig.maxHealth / 3) && distance < attackRange && Time.time - lastTimeUse > coolDown)
        {
            Debug.Log("Can frozen skill");
            return true;
        }
        return false;
    }

    public bool Execute(BossAI boss, CharacterController player)
    {
        lastTimeUse = Time.time;
        boss.SetTriggerAnim("Attack3");
        return true;
    }

 
}
