using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puch : ISkill
{
    float coolDown;
    public float CoolDown => coolDown;

   

    public bool CanUse(BossAI boss, CharacterController player)
    {
        throw new System.NotImplementedException();
    }

    public void Excute(BossAI boss, CharacterController player)
    {
        throw new System.NotImplementedException();
    }
}
