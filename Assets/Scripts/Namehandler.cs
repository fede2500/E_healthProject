using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Namehandler : MonoBehaviour
{
    //public Text Name;
    public InputField NameIn;
    string TextName;
    private string key = "Name";
    private static GameData gameDataInstance;
    
    void Start()
    {
        //TextName = PlayerPrefs.GetString(key);
        NameIn.text = "Name";
        gameDataInstance = GameData.getInstance();

    }

    public void SaveName()
    {
        TextName = NameIn.text;
        PlayerPrefs.SetString(key, TextName);
        gameDataInstance.setPlayerName(TextName);
        SceneManager.LoadScene("Age");
        
    }
    
    
    

        
}