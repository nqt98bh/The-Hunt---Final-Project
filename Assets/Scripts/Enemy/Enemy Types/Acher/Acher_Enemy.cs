using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acher_Enemy : EnemyAI
{
    protected override bool DetectionPlayer()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < config.detectionRange)
        {
            return true;
        }
        return false;
    }

    protected override float GetDirection(bool isChasing)
    {
        return Mathf.Sign(player.position.x -  transform.position.x);
    }
}
