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
        if (data.getPlayer()!=null) {
            player = data.getPlayer();
            playerObj.transform.position = player;
        }
        
        
        dialogue.sentences = new[]
        {
            "Ciao, benvenuto nel gioco!",
            "Sei pronto per una fantastica avventura?",
            "Seguimi! Andiamo!"
        };
        FindObjectOfType<DialogManager>().StartDialogue(dialogue);
    }
     
    
}
