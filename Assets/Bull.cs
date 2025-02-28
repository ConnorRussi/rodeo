using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bull : MonoBehaviour
{
    public float rotateSpeed;
    public const float  defaultRotateSpeed = 50;
    public float maxBuckRotation = 345f;
    public float defaultBullPos = 270f;
    public float minButtonPress = 300f;
    public Vector3 rotate = new Vector3 (0f, 0f, 1f);
    public  bool buckingForward;
    public bool stayOn;
    public bool fall;
    GameManager gameManagerSC;
    GameObject cowboy;
    Vector3 cowboyDefPos;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerSC = GameObject.Find("GameManager").GetComponent<GameManager>();
        cowboy = gameManagerSC.cowboy;
        cowboyDefPos = cowboy.transform.localPosition;
        Restart();
    }
    public void Restart()
    {
        transform.eulerAngles = new Vector3(0, 0, defaultBullPos);
        cowboy.transform.localPosition = cowboyDefPos;
        buckingForward = true;
        fall = false;
        stayOn = true;
        rotateSpeed = defaultRotateSpeed;

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if(!fall) Buck();
           
    }
    void bullAnimation()
    {

        //bull buck animation if not at max rotation
        if (gameObject.transform.eulerAngles.z < maxBuckRotation && buckingForward) gameObject.transform.Rotate(rotate * (rotateSpeed * Time.deltaTime));
        //If we have bucked now resetting to default
        else if (gameObject.transform.eulerAngles.z > defaultBullPos)
        {
            gameObject.transform.Rotate(rotate * (-rotateSpeed * Time.deltaTime));
        }
    }
    void Buck()
    {
        //No longer bucking forward
        if (gameObject.transform.eulerAngles.z > maxBuckRotation && buckingForward) buckingForward = false;
        //made it back to default position so need to reset bucking and start again
        else if (gameObject.transform.eulerAngles.z <= defaultBullPos && !buckingForward)
        {  
            buckingForward = true;
            gameManagerSC.buttonPressed = false;
        }
        bullAnimation();


        //if time for button iteraction turn on logic to stay on handled by game manager
        if (gameObject.transform.eulerAngles.z > minButtonPress && buckingForward && !gameManagerSC.buttonPressed)
        {
            stayOn = false;
        }


        //logic to see if missed timing
        if (!buckingForward && !gameManagerSC.buttonPressed && !stayOn)
        {
            gameManagerSC.GameOver();
        }



    }
}
