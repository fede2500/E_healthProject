using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    GameData data = GameData.getInstance();
    
    public Dialogue dialogue;

    private string savePath = "GameData.json";
    

    private void OnMouseDown()
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        dialogue.sentences = new[] { "Data saved!" };
        FindFirstObjectByType<DialogManager>().StartDialogue(dialogue);

    }
    


}
