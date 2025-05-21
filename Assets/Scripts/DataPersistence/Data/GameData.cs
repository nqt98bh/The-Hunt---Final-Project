using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData 
{
   public CharacterData characterData;
    public int checkPoint;


   
}
[System.Serializable]
public class CharacterData
{
    public int maxHP;
    public int playerDamage;
    public float[] position;
    public CharacterData(CharacterState characterState)
    {
        maxHP = characterState.maxHP;
        playerDamage = characterState.playerDamage;
        position = new float[3];
        position[0] = characterState.transform.position.x;
        position[1] = characterState.transform.position.y;
        position[2] = characterState.transform.position.z;
    }
}


