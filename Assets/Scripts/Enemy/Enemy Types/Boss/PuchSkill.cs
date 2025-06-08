using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuchSkill : ISkill
{
    float coolDown =2f;
    int damage = 50;
    
    public float CoolDown => coolDown;
    float lastTimeUse =0f;

    public bool CanUse(BossAI boss, CharacterController player, float attackRange)
    {

        float distance = Vector3.Distance(player.transform.position,boss.transform.position);
        if(distance < attackRange && Time.time - lastTimeUse > coolDown)
        {
            return true;
            

        }
        return false;
       

    }

    public bool Execute(BossAI boss, CharacterController player)
    {
        lastTimeUse = Time.time;
        boss.SetTriggerAnim("Punch");
        return true;
        
    }

}
