using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour
{
    GameData data = GameData.getInstance();

    private string savePath = "GameData.json";
    
    AsyncOperation asyncLoad;
    public GameObject res;
    
    private void Start()
    {
        if (!File.Exists(savePath))
        {
            res.SetActive(false);
        }
     
    }
    
    public void play()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            asyncLoad = SceneManager.LoadSceneAsync("Name");
        }
        else
        {
            asyncLoad = SceneManager.LoadSceneAsync("Name");
        }
        
    }

    public void resume()
    {
        string loadedJson = File.ReadAllText(savePath);
        data = JsonUtility.FromJson<GameData>(loadedJson);
        asyncLoad = SceneManager.LoadSceneAsync("Room");
    }


}