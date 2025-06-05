using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : EnemyAI
{
    [SerializeField] LayerMask playerLayer;

    protected override bool DetectionPlayer()
    {
        return true;
    }

    protected override float GetDirection(bool isChasing)
    {
        return Mathf.Sign(player.transform.position.x - transform.position.x);
    }



}
