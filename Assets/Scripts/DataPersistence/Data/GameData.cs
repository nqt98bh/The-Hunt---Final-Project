using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData 
{
    public CharacterData characterData;
    public Vector3 lastCheckPoint;
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
    public float speed;
    public int maxHP;
    public float currentHP;
    public int playerDamage;
    public Vector3 playerPosition;
    public CharacterData(CharacterController characterState)
    {
        maxHP = characterState.maxHP;
        playerDamage = characterState.playerDamage;
        currentHP = characterState.GetCurrentHP();
        playerPosition = characterState.transform.position;

    }
}



