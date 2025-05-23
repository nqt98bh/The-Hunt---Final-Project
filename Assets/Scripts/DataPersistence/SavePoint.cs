using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IDataPersistence
{
    
    public void LoadData(GameData data)
    {
      
    }

    public void SaveData(ref GameData data)
    {
        data.lastCheckPoint = this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            DataPersistenceManager.Instance.gameData.lastCheckPoint = transform.position;
            DataPersistenceManager.Instance.SaveGame();
            
        }
    }

}
