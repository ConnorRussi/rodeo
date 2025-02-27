using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject inputFieldGO;
    public InputField userName_ield;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getName()
    {
        return userName_ield.text.ToUpper().ToString();
    }
}
