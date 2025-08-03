using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acher_Enemy : EnemyAIBase
{
    private SpriteRenderer sr;
    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
       
    }
    protected override bool DetectionPlayer()
    {
       
        Vector2 toPlayer = (player.transform.position - transform.position);
        float distance = toPlayer.magnitude; 
        if (distance > config.detectionRange)
        {
            return false;
        }
        
        Vector2 forward = sr.flipX? Vector2.left:Vector2.right; //xác định hướng của enemy

        float dot = Vector2.Dot(toPlayer.normalized, forward); //tính dot product

    

        return (dot > 0);

    }

    protected override float GetDirection(bool isChasing)
    {
        return Mathf.Sign(player.position.x -  transform.position.x);
    }
    protected override void EnemyMovement()
    {
        return;
    }
}
