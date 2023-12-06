using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goExit : MonoBehaviour
{
    GameData data = GameData.getInstance();
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        data.setMinigamePlayed();
        
        SceneManager.LoadScene("FinalScene");
    }
    
    public void goToExit()
    {
        
        data.setMinigamePlayed();
        
        SceneManager.LoadScene("FinalScene");
    }
    
    
}