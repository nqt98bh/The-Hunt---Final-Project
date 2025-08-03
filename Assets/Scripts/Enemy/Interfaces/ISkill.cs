using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    bool CanUse(BossAI boss, CharacterController player, float attackRange);
    bool Execute(BossAI boss, CharacterController player);
    float CoolDown { get; }
}
