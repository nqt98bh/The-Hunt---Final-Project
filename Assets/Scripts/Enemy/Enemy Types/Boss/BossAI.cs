using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Networking.PlayerConnection;
using UnityEditorInternal;
using UnityEngine;

public class BossAI : EnemyAI
{
    [SerializeField] List<ISkill> skillList ;
    [SerializeField] float puchRange = 2f;
    public Vector2 punchSize = new Vector2(1.5f, 0.5f);
    public Transform PuchAttackPoint;

    [SerializeField] float frozenRange = 5f;
    public Transform frozenAttackPoint;

    [SerializeField] float poundRange = 2f;
    public Vector2 poundSize;
    public Transform PoundAttackPoint;

    //freezing skill flag
    public bool isBreakingFreeze = false;

    //Stagger state flag
    private List<float> HPThresholds;
    private bool shouldStagger = false;
    private bool isStaggering = false;

    Vector2Int Vector2Int = new Vector2Int (0, 10);
    private BTNode behaviorTree;

    
    private void Start()
    {
        HPThresholds = new List<float>() { 2f / 3f, 1f / 3f };
        skillList = new List<ISkill>() { new FrozenSkill(), new PoundSkill(), new PunchSkill() };

        Sequence handleFrozenPlayer = new Sequence(
            new Leaf(() => characterController.IsFrozen()),          // nếu player frozen
            new Leaf(() => { BossMovement(); return true; }),    // di chuyển gần (luôn true để BT tiếp)
            new Leaf(() => HandleFrozen()));
   
        Sequence frozenNode = new Sequence(
            new Leaf(() => !isStaggering && !characterController.IsFrozen()),
            new Leaf(() => skillList[0].CanUse(this, characterController, frozenRange)),
            new Leaf(() => skillList[0].Execute(this, characterController)));
        Sequence poundNode = new Sequence(
            new Leaf(() => !isStaggering && skillList[1].CanUse(this, characterController, poundRange)),
            new Leaf(() => skillList[1].Execute(this, characterController)));
        Sequence bossMovement = new Sequence(
            new Leaf(() => !isStaggering && !characterController.IsFrozen() && DetectionPlayer()),
            new Leaf(() => BossMovement()));
        Sequence punchNode = new Sequence(
             new Leaf(() => !isStaggering && skillList[2].CanUse(this, characterController, puchRange)),
             new Leaf(() => skillList[2].Execute(this, characterController)));
        Sequence HandleStaggerStateNode = new Sequence(
            new Leaf(() => !isStaggering && /*isReachHPStagger()*/shouldStagger),
            new Leaf(() => HandleStaggerState(3f)));
            

        behaviorTree = new Selector(
            HandleStaggerStateNode,
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

  
    void isReachHPStagger(int current, int newHP)
    {
        float pre = current / (float)config.maxHealth;
        float n = newHP / (float)config.maxHealth;
        shouldStagger = false;

        for (int i = HPThresholds.Count - 1; i >= 0; i--)
        {
            if (pre > HPThresholds[i] && HPThresholds[i] >= n)
            {
                HPThresholds.Remove(i);
                shouldStagger = true;
                break;

            }

            //if(currentHP == (config.maxHealth*2f/3f) || currentHP == config.maxHealth / 3f)
            //{
            //    return true;
            //}

        }
    }
    public override void TakeDamage(int damage)
    {
      
        int cur = currentHP;
        if (!isStaggering)
        {
            base.TakeDamage(damage);
        }
        else
        {
            currentHP -= damage;
            GameManager.Instance.PlaySoundFX(SoundType.enemyHit);
        }
        
        int newHP = currentHP;
        isReachHPStagger(cur, newHP);
        if (currentHP <= 0)
        {
            GameManager.Instance.GameFinish();
            return;
        }
    }
    private bool HandleStaggerState(float time)  
    {
        animator.SetTrigger("isStagger");
        
        isStaggering = true;
        StartCoroutine(EndStaggerState(time));
        return true;
    }

    IEnumerator EndStaggerState(float time)
    {
        yield return new WaitForSeconds(time);
        isStaggering = false;
        
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
    public void KillBoss()
    {
        GameManager.Instance.GameFinish();
    }
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.attackRange);
        Gizmos.DrawWireSphere(transform.position, config.detectionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(PoundAttackPoint.position, poundSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PoundAttackPoint.position, poundRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(PuchAttackPoint.position, punchSize);



    }

}
