using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData 
{
    public CharacterData characterData;
    public Vector3 checkPoint;
    public int version;
   
    
    public GameData(CharacterData characterData)
    {
        this.characterData = characterData;
        this.version = 0;

    }


}
[System.Serializable]
public class CharacterData
{
    public int maxHP;
    public int playerDamage;
    public Vector3 position;
    public CharacterData(CharacterState characterState)
    {
        maxHP = characterState.maxHP;
        playerDamage = characterState.playerDamage;
        position = characterState.transform.position;
    }
}


