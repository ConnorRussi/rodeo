using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using TMPro;

public class FileDataHandler 
{
    private string dataDirPath = "";

    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Debug.Log(fullPath);
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
                //deserialize data
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError(e + "tried to load data");
            }
        }
        Debug.Log("data loaded from file");
        return loadedData;
        
    }
    public void Save(GameData gameData)
    {
        //used instead of concatination for different OS different seperators
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            //creates a directory the file will be written to if it doesnt already exist
           Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize the c# data
            //We serialize data to turn it from a object to information we can now save to a file\
            //The true formats it in a more readable format but is not required
            string dataToStore = JsonUtility.ToJson(gameData, true);

            //write serialized data to a file
            //ensure connection is closed when we are done with it
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
            Debug.Log("game saved");
        }
        catch (Exception e)
        {
            Debug.LogError(e + "tried to save data");
        }
    }
}
