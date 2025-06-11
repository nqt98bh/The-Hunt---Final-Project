using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditorInternal;
using UnityEngine;

public class BossAI : EnemyAI
{
    [SerializeField] List<ISkill> skillList ;
    [SerializeField] float puchRange = 1.5f;
    public Vector2 punchSize = new Vector2(1.5f, 0.5f);
    public Transform PuchAttackPoint;

    [SerializeField] float frozenRange = 5f;
    public Transform frozenAttackPoint;

    [SerializeField] float poundRange = 2f;
    public float poundSize;
    public Transform PoundAttackPoint;


    Vector2Int Vector2Int = new Vector2Int (0, 10);
    private BTNode behaviorTree;


    private void Start()
    {
        skillList = new List<ISkill>() { new FrozenSkill(), new PoundSkill(), new PunchSkill() };

        behaviorTree = new Selector(
            new Sequence(
                new Leaf(() => skillList[0].CanUse(this, characterController, frozenRange)),
                new Leaf(() => skillList[0].Execute(this, characterController))),
            new Sequence(
                new Leaf(() => skillList[1].CanUse(this, characterController, poundRange)),
                new Leaf(() => skillList[1].Execute(this, characterController))),
            new Sequence(
                new Leaf(() => DetectionPlayer()),
                new Leaf(() => BossMovement()),
            new Sequence(
                new Leaf(() => skillList[2].CanUse(this, characterController, puchRange)),
                new Leaf(() => skillList[2].Execute(this, characterController))))
            );
    }

    protected override void Update()
    {
        AttackPlayer();
       
    }
    protected override void AttackPlayer()
    {
        behaviorTree.Execute();

    }
  
    bool BossMovement()
    {
        EnemyMovement();
        return true;
    }
    public void PunchAttack()
    {
        new PunchSkill().PunchAttack(this, characterController);
    }
    public void FrozenAttack()
    {
        new FrozenSkill().FrozenAttack(this, characterController);

    }
    public void PoundAttack()
    {
        new PoundSkill().PoundAttack(this, characterController);

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
    public void Fire()
    {
        
    }
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,config.attackRange);
        Gizmos.DrawWireSphere(transform.position, config.detectionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(PoundAttackPoint.position, poundSize);
        Gizmos.DrawWireCube(PuchAttackPoint.position, punchSize);


    }

}
