using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    public static DataPersistenceManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Data Persistence manager");
        }
        Instance = this;
    }
    private void Start()
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void SaveGame()
    {
        //To Do: load any game data from a file by using dataHandler
        //If no data can be load, initialize to a new game
        if(gameData == null)
        {
            Debug.Log("No data was found, initialize data as defaults");
            NewGame();
        }
        //To do: push the loaded data to all other scripts which need it
        foreach(IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.SaveData(ref gameData);
        }

    }
    public void LoadGame() 
    {
        //To do: pass the data to other scripts so they can update it
        //To do: save data to a file
        foreach(IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.LoadData(gameData);
        }
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
      
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
