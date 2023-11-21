using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmitSolution : MonoBehaviour
{
    private ScoreManager _scoreManager = ScoreManager.getInstance();
    private bool correct = false;
    public TextMeshProUGUI textToShow;
  
    private void OnMouseDown()
    {
        textToShow.gameObject.SetActive(true);
        if (_scoreManager.getObjectAmount("CogniPik") == 2 && 
            _scoreManager.getObjectAmount("EnigVive") == 1 &&
            _scoreManager.getObjectAmount("Pikachewil") == 1 &&
            _scoreManager.getObjectAmount("CreedHyrulina") == 0 &&
            _scoreManager.getObjectAmount("Ezioflam") == 0 &&
            _scoreManager.getObjectAmount("Creedina") == 0
            )
        {
            textToShow.SetText("You win!");
        }
        else
        {
            textToShow.SetText("Try Again!");
            
        }
    }

   
}
