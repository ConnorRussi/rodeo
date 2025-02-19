using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Linq;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    Bull bullSC;
    public GameObject button;
    public GameObject cowboy;
    public bool buttonPressed;
    public GameObject buttonTextGO;

    public TextMeshProUGUI buttonText;
    public int points;
    public float speedUpMult;
    public float growthStart;
    public int bucks = 0;
    private MyKey[] keys;
    private MyKey selected;
    
    
    struct MyKey
    {
        public string keyName;
        public KeyCode key;

    }
    
    // Start is called before the first frame update
    void Start()
    {
        bullSC = GameObject.Find("Bull").GetComponent<Bull>();
        button = GameObject.Find("keyButton");
        buttonTextGO = GameObject.Find("buttonText");
        buttonText = buttonTextGO.GetComponent<TextMeshProUGUI>();
        button.SetActive(false);
        buttonPressed = false;
        points = 0;
        bucks = 0;
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
        Debug.Log("Earned " + points + " Points");
        
    }
}
