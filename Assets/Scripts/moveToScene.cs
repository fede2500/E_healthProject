using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToScene : MonoBehaviour
{
    private void OnMouseDown()
    {
        
        GameData data = GameData.getInstance();
        GameObject playerObj = GameObject.Find("Player");
        
        data.setPlayer(new Vector2(playerObj.transform.position.x, playerObj.transform.position.y));
        data.setCurrentObjectMinigame(gameObject.name);
        
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TrueFake");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    
}

