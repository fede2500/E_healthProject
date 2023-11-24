using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManage : MonoBehaviour
{
    public Vector2 player;
    
    private BoxCollider2D door;
    private BoxCollider2D computer;
    private BoxCollider2D bookshelf;
    private BoxCollider2D locker;
    private BoxCollider2D tv;
    public Dialogue dialogue;
        
    
    
    
    // Start is called before the first frame update
    void Start()
    {   
        GameObject playerObj = GameObject.Find("Player");
        GameData data = GameData.getInstance();
        
        if (data.getPlayer()!=Vector2.zero) {
            player = data.getPlayer();
            playerObj.transform.position = player;
        }
        
       if( data.isNoMinigamePlayed() )
        {
            dialogue.sentences = new[]
            {
                "Hi (nome)! I'm sorry you're stuck here to take care of me, but I'm really sick.",
                "Anyway Could you book a visit for me? Check on the computer to find a reliable website.",
            };
        }

       //TODO : check dimension of minigamePlayed
       if (data.isMinigamePlayed("Computer"))
       {
           dialogue.sentences = new[]
           {
               "So when is it? ",
               "Tomorrow at 10:30 a.m.",
           };
       }
        
        FindObjectOfType<DialogManager>().StartDialogue(dialogue);
    }
     
    
}
