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
    private void Start()
    {
        if (File.Exists("save.json"))
        {
            // Lettura del file salvato
            string json = File.ReadAllText("save.json");

            // Conversione da JSON a oggetto
            GameData data = JsonUtility.FromJson<GameData>(json);

            // Applicazione delle informazioni per ripristinare la scena
            GameObject textObj = GameObject.Find("scoreText");
            TextMeshProUGUI textMeshProComponent = textObj.GetComponent<TextMeshProUGUI>();
            textMeshProComponent.text = data.numero;

        }
    }

    private void OnMouseDown()
    {
        
        // Creazione di un oggetto da salvare
        GameData data = new GameData();
        GameObject textObj = GameObject.Find("scoreText");
        TextMeshProUGUI textMeshProComponent = textObj.GetComponent<TextMeshProUGUI>();
        data.numero = textMeshProComponent.text;
        // Conversione in formato JSON
        string json = JsonUtility.ToJson(data);

        // Salvataggio su file
        File.WriteAllText("save.json", json);
        
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene_2");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    
}

