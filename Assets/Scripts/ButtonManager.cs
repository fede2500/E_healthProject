using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemoverButton : MonoBehaviour
{
    private ScoreManager _scoreManager = ScoreManager.getInstance();
    public static int score = 0;
    public TextMeshProUGUI textCounter;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        
        
        if (_scoreManager.getSelectedMedicine() != null && _scoreManager.getSelectedMedicine() != "")
        {
            if(gameObject.tag.Equals("RemoveButton"))
            {
                _scoreManager.decreaseSelectedMedicineAmount();
            }
            else
            {
                _scoreManager.incrementSelectedMedicineAmount();
            }
            
            textCounter.text = _scoreManager.getSelectedMedicineAmount().ToString();
        }
        else
        {
            textCounter.text = "0";
        }
        
    }

    private void Update()
    {
        textCounter.text = _scoreManager.getSelectedMedicineAmount().ToString();
    }
}
