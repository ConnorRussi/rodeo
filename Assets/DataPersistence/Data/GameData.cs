using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int lifeTimeBucks;


    //Constructor that sets all default values
    //will call this in new game isntances or when there is no data to load
    public GameData()
    {
        this.lifeTimeBucks = 0;
    }
}
