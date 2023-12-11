using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour
{

    private string savePath = "GameData.json";
    
    AsyncOperation asyncLoad;
    
    private void Start()
    {
        if (!File.Exists(savePath))
        {
            gameObject.SetActive(false);
        }
     
    }
    
   

    public void resume()
    {
        string loadedJson = File.ReadAllText(savePath);
        GameData.getInstance(JsonUtility.FromJson<GameData>(loadedJson));
        asyncLoad = SceneManager.LoadSceneAsync("Room");
    }


}