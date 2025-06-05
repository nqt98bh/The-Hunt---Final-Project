using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    bool CanUse(BossAI boss, CharacterController player);
    void Excute(BossAI boss, CharacterController player);
    float CoolDown { get; }
}
