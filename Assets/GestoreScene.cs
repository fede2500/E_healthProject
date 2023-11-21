using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestoreScene
{
    private static GestoreScene _GestoreSceneInstance;
    private Scene scene;
    private GestoreScene() {} 
 
    public static GestoreScene getInstance() {
        // Crea l'oggetto solo se NON esiste:
        if (_GestoreSceneInstance == null) {
            _GestoreSceneInstance = new GestoreScene();
        }
        return _GestoreSceneInstance;
    }
    // Start is called before the first frame update
    public void setScene(Scene receivedScene)
    {
        scene = receivedScene;
    }

    public Scene getScene()
    {
        return scene;
    }
    
}
