using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogManager>().StartDialogue(dialogue);
    }
    // Start is called before the first frame update
    void Start()
    {
        TriggerDialogue();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
