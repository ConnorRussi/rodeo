using System.Xml.Serialization;
using UnityEngine;

public interface IDataPersistence 
{
    //read only data
    void LoadData(GameData data);
    //pass by refernce so we can modifyt data
    void SaveData(ref GameData data);
}
