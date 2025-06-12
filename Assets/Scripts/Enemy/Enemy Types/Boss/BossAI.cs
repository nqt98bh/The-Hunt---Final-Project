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

    public bool isBreakingFreeze = false;

    Vector2Int Vector2Int = new Vector2Int (0, 10);
    private BTNode behaviorTree;

    private void Start()
    {
        skillList = new List<ISkill>() { new FrozenSkill(), new PoundSkill(), new PunchSkill() };

        Sequence handleFrozenPlayer = new Sequence(
            new Leaf(() => characterController.IsFrozen() == true),          // nếu player frozen
            new Leaf(() => { BossMovement(); return true; }),    // di chuyển gần (luôn true để BT tiếp)
            new Leaf(() => HandleFrozen()));
   
        Sequence frozenNode = new Sequence(
                new Leaf(() => skillList[0].CanUse(this, characterController, frozenRange)),
                new Leaf(() => skillList[0].Execute(this, characterController)));
        Sequence poundNode = new Sequence(
                new Leaf(() => skillList[1].CanUse(this, characterController, poundRange)),
                new Leaf(() => skillList[1].Execute(this, characterController)));
        Sequence bossMovement = new Sequence(
               new Leaf(() => DetectionPlayer()),
               new Leaf(() => BossMovement()));
        Sequence punchNode = new Sequence(
                new Leaf(() => skillList[2].CanUse(this, characterController, puchRange)),
                new Leaf(() => skillList[2].Execute(this, characterController)));
       

        behaviorTree = new Selector(
            handleFrozenPlayer,
            frozenNode,
            poundNode,
            punchNode,
            bossMovement
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
    private bool HandleFrozen()
    {
        if (characterController.IsFrozen() ==false) //nếu player k bị frozen, bỏ qua nhánh này
        {
            isBreakingFreeze = false;
            return false;
        }
        if(isBreakingFreeze) return true; //nếu đang bị đóng băng, giữ lại nhánh

        // Chưa từng thực thi pound để phá băng: kiểm khoảng cách
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist > poundRange)
        {
            return false; // bossMovement đã chạy, BT sẽ quay lại di chuyển
        }
        var pound = skillList.Find(s => s is PoundSkill);
        if (pound != null)
        {
            pound.Execute(this, characterController);
            isBreakingFreeze = true;
        }
        return true;


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
