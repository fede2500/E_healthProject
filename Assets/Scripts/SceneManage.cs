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
            data.setPlayerName("Pingopallo");
            data.setPlayerCluster(2);
            dialogue.sentences = new[]
            {
                "Hi " + data.getPlayerName() + " ! I'm sorry you're stuck here to take care of me, but I'm really sick.",
                "Anyway Could you book a visit for me? Check on the computer to find a reliable website.",
            };
        }

       switch(data.getPrecObjMinigame())
       {
           case "Computer":
               dialogue.sentences = new[]
               {
                   "So when is it? ",
                   "Tomorrow at 10:30 a.m.",
                   "Thank you " + data.getPlayerName() +", now could you switch on the tv please?",
                   "I’d love to watch the never ending show."
               };
               break;
           case "TV":
               if (data.getPlayerCluster() == 2)
               {
                   dialogue.sentences = new[]
                   {
                       "oh yeah, the Never ending show never let’s me down",
                       "For God’s sake! It’s <u>12 a.m.</u> already!!!",
                       "I need to take the pills the doctor prescribed me!",
                       "Could you bring them to me please?",
                       "They’re in one of the drawers between the computer and the bookshelves"
                   };
               }
               else
               {
                   dialogue.sentences = new[]
                   {
                       "oh yeah, the Never ending show never let’s me down",
                       "For God’s sake! It’s 12 a.m. already!!!",
                       "I need to take the pills the doctor prescribed me!",
                       "Could you bring them to me please?",
                       "They’re in one of the drawers between the computer and the bookshelves"
                   };
               }
              
               break;
           case "Locker":
               dialogue.sentences = new[]
               {
                   "Thank you for bringing me the pills, You know what would be awesome right now?",
                   "An amazing book, could you take one from the bookshelves over there?",
                   "You pick, surprise me!"
               };
               break;
           
           case "Bookshelf":
               dialogue.sentences = new[]
               {
                   "Thank you " + data.getPlayerName() + " for the company, you can go now, i’ll be more than fine with my tv on and the book you chose for me. I really appreciated what you did.",
                   "Oh before i forget, The lock on the door isn’t easy, there have been quite some robberies in the neightborhood so i changed it, hope you can unlock it easily, Bye! "
               };
               break;
       }
        
        FindObjectOfType<DialogManager>().StartDialogue(dialogue);
    }
     
    
}
