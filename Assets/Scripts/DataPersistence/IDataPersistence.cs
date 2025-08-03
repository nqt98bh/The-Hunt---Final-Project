using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data);
    void SaveData(ref GameData data); //"ref" used when method need to be modified 
}
