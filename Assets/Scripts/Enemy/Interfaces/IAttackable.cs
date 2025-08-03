using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    void Attack( Transform attackPoint,float radius, CharacterController target, EnemyConfig config);


}
