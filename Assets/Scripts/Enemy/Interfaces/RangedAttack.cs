using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangedAttack : IAttackable
{
    GameObject projectile;
    float force;
    public RangedAttack(GameObject projectile, float force)
    {
        this.projectile = projectile;
        this.force = force;
    }
    public void Attack(Transform attackPoint, float radius, CharacterController target, EnemyConfig config)
    {
        if (target == null) return;
        Vector2 dir = (target.transform.position - attackPoint.transform.position).normalized;
        GameObject projectileGO = GameObject.Instantiate(projectile,attackPoint.position,Quaternion.identity);
        Arrow arrow = projectileGO.GetComponent<Arrow>();
        arrow.damage = config.damage;

        arrow.Fire(dir,force);
    }
}
