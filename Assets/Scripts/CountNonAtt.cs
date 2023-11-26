using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountNonAtt : MonoBehaviour
{
    public int count = 5;
    public TMPro.TextMeshProUGUI countT;
    public GameObject good;
    public GameObject goback;
    
    
    private Dictionary<string, bool> fake_already_checked = new Dictionary<string, bool>();
    
    // Start is called before the first frame update
    public void decreaseCount()
    {
        
            //fake_already_checked.Add(gameObject.name, true);
            count -= 1;

    }

    private void Update()
    {
        
        countT.text = $"{count}";

        if (count == 0)
        {
            good.SetActive(true);
            goback.SetActive(true);
            
        }
    }
}
