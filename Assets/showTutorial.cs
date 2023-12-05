using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showTutorial : MonoBehaviour
{
    public GameObject tutorial; 
    // Start is called before the first frame update
    void Start()
    {
        tutorial.SetActive(true);
    }


    public void hideTutorial()
    {
        tutorial.SetActive(false);
    }
}
