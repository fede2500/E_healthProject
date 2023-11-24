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
        
        AsyncOperation asyncLoad ;

        switch (gameObject.name)
        {
            case "Computer":
                asyncLoad = SceneManager.LoadSceneAsync("Siti_att_nonAtt");
                break;
            case "TV":
                asyncLoad = SceneManager.LoadSceneAsync("Profiling_anx _pix");
                break;
            case "Locker":
                asyncLoad = SceneManager.LoadSceneAsync("PharmaScene");
                break;
            case "Bookshelf":
                asyncLoad = SceneManager.LoadSceneAsync("Menu");
                break;
            
        }
    }
    
}

