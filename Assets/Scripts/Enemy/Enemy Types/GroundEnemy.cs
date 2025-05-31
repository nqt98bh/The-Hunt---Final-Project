using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundEnemy : EnemyAI
{
    [Header("Raycast & LayerMask")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask WallLayer;
    [SerializeField] LayerMask playerLayer;

    [Header("Check Transforms")]
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform frontCheck;
 
    protected override bool DetectionPlayer()
    {
        if (player == null) return false;
        Vector2 target = player.position + Vector3.up * 0.5f;
        Vector2 direction = new Vector2((target.x - transform.position.x), 0f).normalized; //Đảm bảo raycast luôn hướng về player

        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), direction, config.detectionRange, playerLayer | WallLayer);
        Debug.DrawRay(transform.position + new Vector3(0f, 0.5f, 0f), direction * config.detectionRange, Color.green);
        bool seePlayer = hitPlayer.collider != null && hitPlayer.collider.transform == player;
        if (seePlayer)
        {
            return true;
        }
        return false;

    }

    protected override float GetDirection(bool isChasing)
    {
        if (!isChasing)
        {
            
            if(!OnGrounded() || ObstacleAhead()) 
            {
                //facingRight = !facingRight;
                //return facingRight ? 1f : -1f;
                Flip();

            }

            return facingRight ? 1f : -1f;
        }
        else
        {
            //FacePlayer();
            return Mathf.Sign(player.position.x - transform.position.x);
        }
        
    }
    protected bool OnGrounded()
    {
        bool isGround = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, groundLayer);
        return isGround;
    }
    protected bool ObstacleAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(frontCheck.position, facingRight ? Vector2.right : Vector2.left, 0.5f, WallLayer);
        return hit.collider != null;
    }
    protected override void OnDrawGizmosSelected()
    {
        if (OnGrounded())
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(groundCheck.position, Vector2.down * 0.2f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(groundCheck.position, Vector2.down * 0.2f);
        }

        if (ObstacleAhead())
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(frontCheck.position, (facingRight ? Vector2.right : Vector2.left) * 0.5f);

        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(frontCheck.position, (facingRight ? Vector2.right : Vector2.left) * 0.5f);

        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.detectionRange);
        if (DetectionPlayer() && player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, player.position);
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);

    }



}
