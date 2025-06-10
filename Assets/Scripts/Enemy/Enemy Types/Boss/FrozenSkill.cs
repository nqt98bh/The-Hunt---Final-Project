using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenSkill : ISkill
{
    float coolDown = 2f;
    public float CoolDown =>coolDown;
    float lastTimeUse = 0f;
    //private BossAI boss;
    //private CharacterController player;
    //private bool hasExecuted;
    public bool CanUse(BossAI boss, CharacterController player, float attackRange)
    {
        float distance = Vector2.Distance(player.transform.position,boss.transform.position);
        EnemyConfig bossConfig = boss.GetConfig();

        if ((boss.GetCurrentHP() < (bossConfig.maxHealth * 2 / 3) &&boss.GetCurrentHP() >= (bossConfig.maxHealth/3)) && distance < attackRange && Time.time - lastTimeUse > coolDown ) 
        {
            Debug.Log("Can frozen skill");
            return true;
        }
        return false;
    }

    public bool Execute(BossAI boss, CharacterController player)
    {
        lastTimeUse = Time.time;
       
        boss.SetTriggerAnim("Attack2");

        //EnemyConfig bossConfig = boss.GetConfig();
        //float dir = boss.transform.localScale.x;
        //GameObject frozenGO = GameObject.Instantiate(bossConfig.projectilePrefab, boss.attackFrozen.transform.position, Quaternion.identity);
        //frozenGO.transform.localScale = new Vector3(dir, frozenGO.transform.localScale.y, frozenGO.transform.localScale.z);
        //Frozen frozen = frozenGO.GetComponent<Frozen>();
        //frozen.Initialize(bossConfig.projectileSpeed, dir, player.transform);
        return true;
    }
    

    //public void Attack(Transform attackPoint, float radius, CharacterController target, EnemyConfig config)
    //{
       
    //    float dir = boss.transform.localScale.x;
    //    GameObject frozenGO = GameObject.Instantiate(bossConfig.projectilePrefab, boss.attackFrozen.transform.position, Quaternion.identity);
    //    frozenGO.transform.localScale = new Vector3(dir, frozenGO.transform.localScale.y, frozenGO.transform.localScale.z);
    //    Frozen frozen = frozenGO.GetComponent<Frozen>();
    //    frozen.Initialize(bossConfig.projectileSpeed, dir, player.transform);
     
    //}
}
