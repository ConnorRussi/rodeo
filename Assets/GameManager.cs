using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Bull bullSC;
    public GameObject button;
    public GameObject cowboy;
    public bool buttonPressed;
    public int points;
    public float speedUpMult;
    public float growthStart;
    public int bucks = 0;
    public KeyCode[] keys;
    

    
    // Start is called before the first frame update
    void Start()
    {
        bullSC = GameObject.Find("Bull").GetComponent<Bull>();
        button.SetActive(false);
        buttonPressed = false;
        points = 0;
        bucks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bullSC.stayOn) StayOn();
    }
    public void StayOn()
    {
        //Debug.Log("stayON");
        if (!bullSC.stayOn && !buttonPressed)
        {
            button.gameObject.SetActive(true);
            checkButtonPressed(KeyCode.Space);
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
        Debug.Log("Earned " + points + " Points");
    }
}
