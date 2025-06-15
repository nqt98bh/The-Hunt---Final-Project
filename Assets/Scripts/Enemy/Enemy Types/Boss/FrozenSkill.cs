using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenSkill : ISkill
{
    float coolDown = 5f;
    public float CoolDown =>coolDown;
    float lastTimeUse = 0f;
   
    public bool CanUse(BossAI boss, CharacterController player, float attackRange)
    {
        float distance = Vector2.Distance(player.transform.position,boss.transform.position);
        EnemyConfig bossConfig = boss.GetConfig();

        float hp = boss.GetCurrentHP();
        float maxHp = bossConfig.maxHealth;
        // health 2/3 -> 1/3: i.e. 0.33–0.66 fraction
        if (hp < (maxHp * 2f / 3f) && hp >= (maxHp / 3f) && distance < attackRange && Time.time - lastTimeUse > coolDown)
        {
            Debug.Log("Can frozen");
            return true;
        }
        return false;
    }

    public bool Execute(BossAI boss, CharacterController player)
    {
        lastTimeUse = Time.time;
        boss.SetTriggerAnim("Attack2");
        return true;
    }

    public void FrozenAttack(BossAI boss, CharacterController player)
    {
        EnemyConfig bossConfig = boss.GetConfig();
        float dir = boss.transform.localScale.x;
        GameObject frozenGO = GameObject.Instantiate(bossConfig.projectilePrefab, boss.frozenAttackPoint.transform.position, Quaternion.identity);
        frozenGO.transform.localScale = new Vector3(dir, frozenGO.transform.localScale.y, frozenGO.transform.localScale.z);
        FrozenProjectile frozen = frozenGO.GetComponent<FrozenProjectile>();
        frozen.Initialize(bossConfig.projectileSpeed, dir, player.transform,boss);
    }

}
