using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Medicine : MonoBehaviour
{
    
    public TextMeshProUGUI textToShow;
    private ScoreManager _scoreManager = ScoreManager.getInstance();
    
    
    
    private void OnMouseDown()
    {
        string message = "";
        switch (gameObject.name)
        {
            case "Pikachewil":
                message = "Pikachewil p.a. Pokevitamin 300mg";
                break;
            case "Puzzletrex":
                message = "Puzzletrex p.a. Laytonium 200mg";
                break;
            case "Hyrulex":
                message = "Hyrulex p.a. Linkazol 100mg";
                break;
            case "Sonicillin":
                message = "Sonicillin p.a. Speedamine 150mg";
                break;
            case "Ezioflam":
                message = "Ezioflam p.a. Creedoxin 130mg";
                break;
            case "Kooparil":
                message = "Kooparil p.a. Mushrotonin 70mg";
                break;
        }
        MostraIlMessaggio(message);
        _scoreManager.setSelectedMedicine(gameObject.name);
        
    }
    
    void MostraIlMessaggio(string testoDelMessaggio)
     {
            textToShow.text = testoDelMessaggio;
     }

}
