using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler 
{
    private string dataPath = "";
    private string dataFileName = "saveFile.json";

    public FileDataHandler (string dataPath, string dataFileName)
    {
        this.dataPath = dataPath;
        this.dataFileName = dataFileName;
    }
    public GameData Load()
    {
        string fullPath = Path.Combine(dataPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e.Message);
            }
        }

        
        return loadedData;
    }
    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataPath, dataFileName);
        try
        {
            //create a directory the file will be written if it doent already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialization c# game data object to json

            string json = JsonUtility.ToJson(data,true);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(json);
                }
            }
            //File.WriteAllText(fullPath, json);
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath +"\n"+ e.Message);
        }
    }
}
