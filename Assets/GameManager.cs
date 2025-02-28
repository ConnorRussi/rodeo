using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Linq;
using Random = UnityEngine.Random;
using System.Data;
using Mono.Cecil;

public class GameManager : MonoBehaviour
{
    Bull bullSC;
    public GameObject button;
    public GameObject resButt;
    public GameObject cowboy;
    public GameObject bullGO;
    public bool buttonPressed;
    public GameObject buttonTextGO;
    public GameObject User_NameField;
    public GameObject namePtsDisplayGO;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI namePtsDisplay;
    public int points;
    public float speedUpMult;
    public float growthStart;
    public int bucks = 0;
    private MyKey[] keys;
    private MyKey selected;

    public ScoreLine[] topTen = new ScoreLine[5];
    public struct ScoreLine
    {
        public int score;
        public string name;
        public ScoreLine(int i, string s)
        {
            this.score = i;
            this.name = s;
        }
    }
    
    
    struct MyKey
    {
        public string keyName;
        public KeyCode key;

    }
    
    // Start is called before the first frame update
    void Awake()
    {
        bullSC = GameObject.Find("Bull").GetComponent<Bull>();
        button = GameObject.Find("keyButton");
        buttonTextGO = GameObject.Find("buttonText");
        buttonText = buttonTextGO.GetComponent<TextMeshProUGUI>();
        button.SetActive(false);
        buttonPressed = false;
        User_NameField.SetActive(false);
        namePtsDisplayGO.SetActive(false);
        points = 0;
        bucks = 0;
        SetKeys();
        resButt = GameObject.Find("Restart_Button");
        resButt.SetActive(false);
        
    }
    
    void setTopTen()
    {
        //will read top ten from file here and write them into the scores
        for (int i = 0; i < topTen.Length; i++)
        {
            topTen[i] = new ScoreLine(0, "___");
        }
    }
    void SetKeys()
    {
        keys = new MyKey[2];
        MyKey key0 = new MyKey();
        key0.key = KeyCode.RightArrow;
        key0.keyName = "->";
        keys[0] = key0;
        MyKey key1 = new MyKey();
        key1.key = KeyCode.LeftArrow;
        key1.keyName = "<-";
        keys[1] = key1;
        selected = keys[0];
    }
    // Update is called once per frame
    void Update()
    {

        if (!bullSC.stayOn && !bullSC.fall) StayOn();
        else if (!bullSC.fall) selected = keys[Random.Range(0, (keys.Length))];
    }
    public void StayOn()
    {
        //Debug.Log("stayON");
        if (!bullSC.stayOn && !buttonPressed)
        {
            button.gameObject.SetActive(true);
            buttonText.SetText(selected.keyName);
            checkButtonPressed(selected.key);
        }
        
         
        
    }
    void checkButtonPressed(KeyCode keyCode)
    {
        if (Input.anyKeyDown  && !bullSC.fall) {
            if (Input.GetKeyDown(keyCode))
            {
                bullSC.stayOn = true;
                button.SetActive(false);
                buttonPressed = true;
                bucks++;
                points += Mathf.FloorToInt(bullSC.rotateSpeed);
                bullSC.rotateSpeed += (growthStart * speedUpMult);
                growthStart *= speedUpMult;
                
            }
            else if (!Input.GetKeyDown(keyCode)) GameOver();
        }
        
    }
    public void GameOver()
    {
        bullSC.fall = true;
        cowboy.transform.position = Vector3.zero;
        button.SetActive(false);
        User_NameField.SetActive(true);
        bullSC.rotateSpeed = 0;
        
    }
    public void Restart()
    {
        button.SetActive(true);
        namePtsDisplayGO.SetActive(false);
        resButt.SetActive(false);
        bullSC.Restart();
        points = 0;

    }
    public void DisplayInfo(string s)
    {

        
        namePtsDisplayGO.SetActive(true);
        
        namePtsDisplay.SetText(SetScoreDisplay(s, points));
        resButt.SetActive(true);
        User_NameField.SetActive(false);
    }
    private string SetScoreDisplay(string s, int pts)
    {
        SortTopTen(new ScoreLine(pts, s));
        string retVal = "";        
        for (int i = 0; i < topTen.Length; i++)
        {
            retVal += (topTen[i].name + " " + topTen[i].score + "Points \n");
        }
        return retVal;
    }
    private void SortTopTen(ScoreLine s)
    {
        for (int i = 0; i < topTen.Length; i++)
        {
            if (s.score >= topTen[i].score)
            {
                bump(i, s);
                break;
            }
        }
    }
    private void bump(int index, ScoreLine s)
    {
        for(int j = index; j < topTen.Length; j++)
        {
            ScoreLine temp = topTen[j];
            topTen[j] = s;
            s = temp;
        }
    }
}
