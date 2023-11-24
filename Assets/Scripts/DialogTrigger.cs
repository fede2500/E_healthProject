using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    private GameData data = GameData.getInstance();
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        if (data.isMinigamePlayed(gameObject.name))
        {
            dialogue.sentences = new[] { "You already played this minigame!" };
        }
        else
        {
            switch (gameObject.name)
            {
                case "Computer":
                    dialogue.sentences = new[] { "Use the computer!" };
                    break;
                case "TV":
                    dialogue.sentences = new[] { "Use the TV!" };
                    break;
                case "Locker":
                    dialogue.sentences = new[] { "Use the Locker!" };
                    break;
                case "Bookshelf":
                    dialogue.sentences = new[] { "Use the Bookshelf!" };
                    break;
            }
        }

        FindObjectOfType<DialogManager>().StartDialogue(dialogue);
        
    }
    // Start is called before the first frame update
    
    private void OnCollisionEnter2D()
    {
            TriggerDialogue();
    }

    // Update is called once per frame
    void Start()
    {
        
    }
}
