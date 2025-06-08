using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditorInternal;
using UnityEngine;

public class BossAI : EnemyAI
{
    [SerializeField] List<ISkill> skill;
    float puchRange = 5f;
    Vector2Int Vector2Int = new Vector2Int (0, 10);
    private BTNode behaviorTree;

   
    private void Start()
    {
        skill = new List<ISkill>() { new PuchSkill() };

        behaviorTree = new Selector(new Leaf(() => skill[0].CanUse(this, characterController, puchRange)), new Leaf(() => skill[0].Execute(this, characterController)));
    }
    protected override void Update()
    {
        AttackPlayer();

    }

    protected override void AttackPlayer()
    {
        behaviorTree.Excute();

    }
    protected override bool DetectionPlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < config.detectionRange)
        {
            return true;
        }
        return false;
    }
    protected override float GetDirection(bool isChasing)
    {
        if (!isChasing) return 0;
        return Mathf.Sign(player.position.x - transform.position.x);
    }

    public void SetTriggerAnim(string animName)
    {
        animator.SetTrigger(animName);
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,config.attackRange);
        Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
        Gizmos.DrawWireSphere(transform.position, config.detectionRange);

    }

}
