using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject start;
    public GameObject back;
    public GameObject title;
    public GameObject description;

    private GameData data;

    private void Start()
    {
        data = GameData.getInstance();
        Debug.Log(data.getQuizPlayed());
        if (data.getQuizPlayed())
        {
            back.SetActive(true);
            start.SetActive(false);
            title.SetActive(false);
            description.SetActive(false);
        }
        else
        {
            back.SetActive(false);
            start.SetActive(true);
            title.SetActive(true);
            description.SetActive(true);
        }
        
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
