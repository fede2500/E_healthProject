using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmitSolution : MonoBehaviour
{
    private ScoreManager _scoreManager = ScoreManager.getInstance();
    private bool correct = false;
    public TextMeshProUGUI textToShow, receptText;
    public GameObject end;
    private GameData data;
    private int attempts = 0;

    private void Start()
    {
        data = GameData.getInstance();
    }

    private void OnMouseDown()
    {
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
            if (data.getPlayerCluster() == 2)
            {
                if (attempts == 0)
                {
                    textToShow.SetText("Try Again! Remember: it is almost lunch time");
                    attempts++;
                }
                else
                {
                    textToShow.SetText("Try Again! Remember: it is almost lunch time");
                    receptText.SetText("\"Take 70 mg of creedoxin twice a day, <u>after meals</u>. Thereafter, take 85 mg of pokevitamin twice a day <u>before meals</u>. Finally, take 50 mg of laytonium every day, 30 minutes <u>before a meal.</u>\";");
                    
                }
            }
            else
            {
                textToShow.SetText("Try Again!");
            }
            
            
        }
    }

   
}
