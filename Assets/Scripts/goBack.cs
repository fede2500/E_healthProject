using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goBack : MonoBehaviour
{
    GameData data = GameData.getInstance();
    // Start is called before the first frame update
    private void OnMouseDown()
    {

        data.setMinigamePlayed();
        
        SceneManager.LoadScene(0);
        
    }
}
