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
            case "CogniPik":
                message = "CogniPik p.a. Pokevitamin: 25 mg\nLaytonium: 15 mg";
                break;
            case "EnigVive":
                message = "EnigVive p.a. Pokevitamin: 15 mg\nLaytonium: 20 mg";
                break;
            case "CreedHyrulina":
                message = "CreedHyrulina p.a. Linkazol: 25 mg\nLaytonium: 10 mg";
                break;
            case "Pikachewil":
                message = "Pikachewil p.a. Pokevitamin 20 mg";
                break;
            case "Ezioflam":
                message = "Ezioflam p.a. Linkazol: 60 mg\nCreedoxin: 18 mg";
                break;
            case "Creedina":
                message = "Creedina p.a. Creedoxin: 35 mg";
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
