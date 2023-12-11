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
        Debug.Log(data.isNoMinigamePlayed());
       if(!data.isMinigamePlayed("Computer"))
        {
            
            data.setPlayerName($"{data.getPlayerName()}");
            dialogue.sentences = new[]
            {
                "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine + "Hi " + data.getPlayerName() + "! I'm sorry you're stuck here to take care of me, but I'm really sick.",
                "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine +"Anyway Could you book a visit for me? Check on the computer to find a reliable website.",
            };
        }

       switch(data.getPrecObjMinigame())
       {
           case "Computer":
               dialogue.sentences = new[]
               {
                   "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine +"So when is it? ",
                   $"<u>{data.getPlayerName()}:</u>" + System.Environment.NewLine+ System.Environment.NewLine +"Tomorrow at 10:30 a.m.",
                   "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine +"Thank you " + data.getPlayerName() +", now could you switch on the tv please? I’d love to watch the Never Ending Show.",
                   "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine +"Oh my God! Look at that old joystick! Before putting on my show, try to play at some videogames to see if it still works."
               };
               break;
           case "TV":
               if (data.getPlayerCluster() == 2)
               {
                   dialogue.sentences = new[]
                   {
                       "<u>Andrea:</u>" + System.Environment.NewLine +System.Environment.NewLine + "oh yeah, the Never ending show never let’s me down",
                       "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine +"For God’s sake! It’s <u>12 a.m.</u> already!!!",
                       "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine +"I need to take the pills the doctor prescribed me!",
                       "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine +"Could you bring them to me please?",
                       "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine +"They’re in one of the drawers between the computer and the bookshelves"
                   };
               }
               else
               {
                   dialogue.sentences = new[]
                   {
                       "<u>Andrea:</u>" + System.Environment.NewLine +System.Environment.NewLine + "oh yeah, the Never ending show never let’s me down",
                       "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine +"For God’s sake! It’s 12 a.m. already!!!",
                       "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine +"I need to take the pills the doctor prescribed me!",
                       "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine +"Could you bring them to me please?",
                       "<u>Andrea:</u>" + System.Environment.NewLine +System.Environment.NewLine + "They’re in one of the drawers between the computer and the bookshelves"
                   };
               }
              
               break;
           case "Locker":
               dialogue.sentences = new[]
               {
                   "<u>Andrea:</u>" + System.Environment.NewLine +System.Environment.NewLine +"Thank you for bringing me the pills, You know what would be awesome right now?",
                   "<u>Andrea:</u>" + System.Environment.NewLine +System.Environment.NewLine +"An amazing book, could you take one from the bookshelves over there?",
                   $"<u>{data.getPlayerName()}:</u>" + System.Environment.NewLine +System.Environment.NewLine +"What kind of book would you like to read?",
                   "<u>Andrea:</u>" + System.Environment.NewLine +System.Environment.NewLine +"You pick, surprise me!"
               };
               break;
           
           case "Bookshelf":
               dialogue.sentences = new[]
               {
                   "<u>Andrea:</u>" + System.Environment.NewLine +System.Environment.NewLine + "Thank you " + data.getPlayerName() + " for the company, you can go now, i’ll be more than fine with my tv on and the book you chose for me. I really appreciated what you did.",
                   "<u>Andrea:</u>" + System.Environment.NewLine + System.Environment.NewLine +"Oh before i forget, The lock on the door isn’t easy, there have been quite some robberies in the neighborhood so i changed it, hope you can unlock it easily, Bye! "
               };
               break;
       }
        
        FindFirstObjectByType<DialogManager>().StartDialogue(dialogue);
    }
     
    
}
