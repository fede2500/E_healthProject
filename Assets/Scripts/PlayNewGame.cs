using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using UnityEngine.SceneManagement;

    public class PlayNewGame : MonoBehaviour
    {
        private string savePath = "GameData.json";
    
        AsyncOperation asyncLoad;
        
        
        public void play()
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            
            asyncLoad = SceneManager.LoadSceneAsync("Name");
        
        }
    }
