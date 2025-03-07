using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler fileDataHandler;

    //sets this instance to be allowed to get data publicly but privatley set data
   public static DataPersistenceManager Instance{  get; private set; }

    


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("more than one data persistence manager in sccene");
        }
        Instance = this;
        
    }
    private void Start()
    {
        //Gives it a standard position to save everything
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        Loadgame();

    }

    public void Newgame()
    {
        this.gameData = new GameData();
    }

    public void Loadgame()
    {
        //Load any saved data from a file usinbg a data handler
        this.gameData = fileDataHandler.load();
        //if no data instaialize to a new game
        if (this.gameData == null)
        {
            Debug.Log("no data found to load so setign to defauls");
            Newgame();
        }
        //push loaded data to all other scripts that need it
        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            obj.LoadData(gameData);
        }
    }

    public void Savegame()
    {
        //pass datra to other scripts so they can update it
        
        foreach(IDataPersistence obj in dataPersistenceObjects)
        {
            obj.SaveData(ref gameData);
        }
        //save that dfata to a file using the data handler
        fileDataHandler.Save(gameData);

    }
    private void OnApplicationQuit()
    {
        Savegame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
            
    }
}
