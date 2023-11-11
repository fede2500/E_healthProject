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
        if (_scoreManager.getObjectAmount("Pikachewil") == 3 && 
            _scoreManager.getObjectAmount("Puzzletrex") == 2)
        {
            textToShow.SetText("You win!");
        }
        else
        {
            textToShow.SetText("Try Again!");
            
        }
    }

   
}
