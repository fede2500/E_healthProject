using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    private GameData data = GameData.getInstance();
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        if (data.isMinigamePlayed(gameObject.name))
        {
            //dialogue.sentences = new[] { "You already played this minigame!" };
            //FindObjectOfType<DialogManager>().StartDialogue(dialogue);
        }
        else
        {
            if (gameObject.name.Equals(data.getFirstGameName()))
            {
                
                dialogue.sentences = new[] { "Use the " + gameObject.name + " !" };
                
            }
            else
            {
                if (data.isMinigamePlayed(data.getPrecedentGameName(gameObject.name)))
                {
                    dialogue.sentences = new[] { "Use the " + gameObject.name + " !" };
                }
                else
                {
                    dialogue.sentences = new[] { "You still need to unlock this minigame" };
                }
            }
            
            FindObjectOfType<DialogManager>().StartDialogue(dialogue);
          
        }

        
        
    }
    // Start is called before the first frame update

    public void dialogSite()
    {
        switch (data.getPlayerCluster())
        {
            case 1:
                dialogue.sentences = new[]
                {
                    "Are you sure that’s the right website?",
                    "Try to have a look to some details: would you share your credit card number wiht a site?"
                };
                break;
            default:
                dialogue.sentences = new[] { "Are you sure that’s the right website?" };
                break;

        }
        
        FindObjectOfType<DialogManager>().StartDialogue(dialogue);
    }
    
    private void OnCollisionEnter2D()
    {
            TriggerDialogue();
    }

    // Update is called once per frame
    void Start()
    {
        
    }
}
