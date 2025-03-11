using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements.Experimental;
using System.Diagnostics;
using JetBrains.Annotations;
using static GameData;

[System.Serializable]
public class GameData: ISerializationCallbackReceiver
{
    public int lifeTimeBucks;
    public ScoreLine[] topTen = new ScoreLine[5];
    public class ScoreLine
    {
        public int score;
        public string name;
        public ScoreLine(int i, string s)
        {
            this.score = i;
            this.name = s;
           
        }
        public ScoreLine() 
        {
            this.name = "___";
            this.score = 0;
        }
       
    }

   

   
    [SerializeField] private List<int> scores;
    [SerializeField] private List<string> names;
    public void OnBeforeSerialize()
    {
        scores.Clear();
        names.Clear();
        for (int i = 0; i < topTen.Length; i++)
        {
            scores.Add(topTen[i].score);
            names.Add(topTen[i].name);
        }
            
    }
    public void OnAfterDeserialize() 
    {
        //topTen = new ScoreLine[scores.Count];
        if(scores.Count != topTen.Length)
        {
            for (int i = 0; i < topTen.Length; i++)
            {
                topTen[i] = new ScoreLine(0, "___");
            }
            return;
        }
        for (int i = 0; i < topTen.Length; i++)
        {
           topTen[i] = new ScoreLine(scores[i], names[i]);
        }
        
    }
       
    //Constructor that sets all default values
    //will call this in new game isntances or when there is no data to load
    public GameData()
    {
        this.lifeTimeBucks = 0;
        this.topTen = new ScoreLine[5];
        for (int i = 0; i < this.topTen.Length; i++)
        {
            this.topTen[i] = new ScoreLine();
        }

    }
   
}
