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

    private void OnMouseDown()
    {

        GameData data = GameData.getInstance();
        GameObject playerObj = GameObject.Find("Player");


        data.setPlayer(new Vector2(playerObj.transform.position.x, playerObj.transform.position.y));
        data.setCurrentObjectMinigame(gameObject.name);

        hittable = setHittable();

        AsyncOperation asyncLoad;

        if (hittable)
        {
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

