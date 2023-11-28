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

public class Agehandler : MonoBehaviour
{
    //public Text Age;
    public InputField NumberIn;
    private string key = "Age";

    void Start()
    {
        NumberIn.text = "Age";
    }

    public void SaveAge()
    {
        int i=int.Parse(NumberIn.text);
        PlayerPrefs.SetInt(key, i);
        //PlayerPrefs.Save();
        SceneManager.LoadScene("PersQuestions");
    }

        
}
