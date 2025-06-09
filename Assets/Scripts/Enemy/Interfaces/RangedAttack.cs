using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangedAttack : IAttackable
{
    GameObject projectilePrefab;
    float speed;
    public RangedAttack(GameObject projectile, float speed)
    {
        this.projectilePrefab = projectile;
        this.speed = speed;
    }
    public void Attack(Transform attackPoint, float radius, CharacterController target, EnemyConfig config)
    {
        Debug.Log("Ranged Attack)");
        if (target == null || projectilePrefab == null) return;

        Vector3 p0 = attackPoint.position;
        Vector3 p2 = target.transform.position;

        float distance = Vector3.Distance(p0, p2); //get distance from enemy to player

        // Derive travel duration = distance / projectileSpeed
        //    (Add a minimum so never get zero or extremely tiny durations.)
        float duration = Mathf.Max(0.1f, distance / speed);

        //Derive arc height as a fraction of distance
        //0.5×distance, but clamp to a minimum (1f) for very short shots
        float arcHeight = Mathf.Max(1f, distance * 0.5f);

        //calcuclate bezier mid point
        Vector3 mid = (p0 + p2) * 0.5f;

        Vector3 p1 = mid +Vector3.up * arcHeight;

        //Instantiate prefab at attackPoint
        GameObject arrowGO = GameObject.Instantiate(projectilePrefab,p0,Quaternion.identity);

        var arrow = arrowGO.GetComponent<Arrow>();
        arrow.Initialize(p0, p1, p2, duration, config.damage);


    }
}
