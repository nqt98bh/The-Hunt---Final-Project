using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StrategyFactory
{
  public static IMoveable CreateMovement(MovementType movementType)
    {
        switch (movementType)
        {
            case MovementType.None: return new NoneMovement();
            case MovementType.Ground: return new GroundMovement();
            case MovementType.Fly: return new FlyMovement();
            default: throw new ArgumentException();
        }
    }
    public static IAttackable CreateAttack(AttackType attackType,EnemyConfig config)
    {
        switch(attackType)
        {
            case AttackType.Melee: return new MeleeAttack();
            case AttackType.Ranged: return new RangedAttack(config.projectilePrefab,config.projectileSpeed);
                default: throw new ArgumentException();
        }
    }
}
