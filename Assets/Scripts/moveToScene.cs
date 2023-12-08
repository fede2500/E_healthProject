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
    private bool hittable;
    GameData data = GameData.getInstance();
    

    private void OnMouseDown()
    {
        
        hittable = setHittable();

        if (hittable)
        {
            
            GameObject playerObj = GameObject.Find("Player");
            AsyncOperation asyncLoad;
            data.setPlayer(new Vector2(playerObj.transform.position.x, playerObj.transform.position.y));
            data.setCurrentObjectMinigame(gameObject.name);
            
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
                    data.setQuizPlayed(false);
                    asyncLoad = SceneManager.LoadSceneAsync("Menu");
                    break;
                case "Door":

                    asyncLoad = SceneManager.LoadSceneAsync("Blocks");
                    break;

            }
        }

    }

    private bool setHittable()
    {
        GameData data = GameData.getInstance();

        if (data.isMinigamePlayed(gameObject.name))
        {
            return false;
        }
        else
        {
            if (gameObject.name.Equals(data.getFirstGameName()))
            {
                return true;
            }
            else
            {
                if (data.isMinigamePlayed(data.getPrecedentGameName(gameObject.name)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}

