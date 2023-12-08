using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    GameData data = GameData.getInstance();

    private string savePath = "GameData.json";
    

    private void OnMouseDown()
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("data saved");
    }
    


}
