using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.U2D.Animation;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    private const string kSaveKey = "save_data";

    [SerializeField]private CharacterState characterState;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    //private FileDataHandler dataHandler;

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
        //this.dataHandler = new FileDataHandler(Application.persistentDataPath,fileName); 
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        CharacterData characterData = new CharacterData(characterState);
        this.gameData = new GameData(characterData);
    }
    public void SaveGame()
    {

        //To do: pass the data to other scripts so they can update it
        //To do: save data to a file

        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.SaveData(ref gameData);
        }
        string json = JsonUtility.ToJson(gameData,true);
        PlayerPrefs.SetString(kSaveKey, json);
        PlayerPrefs.Save();
        //this.dataHandler.Save(gameData);


    }
    public void LoadGame() 
    {

        //To Do: load any game data from a file by using dataHandler
        //this.gameData = dataHandler.Load();
        //If no data can be load, initialize to a new game
        if (gameData == null)
        {
            Debug.Log("No data was found, initialize data as defaults");
            NewGame();
        }
        if (PlayerPrefs.HasKey(kSaveKey))
        {
            string json = PlayerPrefs.GetString(kSaveKey);
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            NewGame();
            Debug.Log("No PlayerPrefs save found - creating new game");
        }
            //To do: push the loaded data to all other scripts which need it
        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.LoadData(gameData);
        }
    }
    
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
      
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
