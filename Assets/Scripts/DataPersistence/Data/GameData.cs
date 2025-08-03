using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData 
{
    public CharacterData characterData;
    public int version;
    public int CoinValue;
  
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
    public int currentHP;
    public int playerDamage;
    public Vector3 playerPosition;
    public CharacterData(CharacterController characterState)
    {
        if (characterState == null)
        {
            maxHP = 100;
            playerDamage = 20;
            currentHP = maxHP;
            playerPosition = new Vector3(-3, -4f, 0);
        }
        else
        {


            maxHP = characterState.maxHP;
            playerDamage = characterState.playerDamage;
            currentHP = characterState.maxHP;
            playerPosition = characterState.transform.position;
        }

    }
}



