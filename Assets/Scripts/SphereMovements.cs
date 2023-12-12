using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SphereMovements : MonoBehaviour
{
    public TextMeshProUGUI textToShow, timeT, tutorialText;
    public Collider2D finishBlock;
    private bool isSelected = false;
    private bool win = false;
    public GameObject goBack, tryAgain, tutorial, checkpoint1, checkpoint2, checkpoint3;
    private Vector2 movement;
    
    private GameData data;
    private float timeRemaining = 240f;

    private void Start()
    {
        data = GameData.getInstance();
        if (data.getPlayerCluster() != 0)
        {
            checkpoint1.SetActive(false);
            checkpoint2.SetActive(false);
            checkpoint3.SetActive(false);
            tutorialText.text =
                "To leave the room, you must move the ball to the opposite square. Click on a block or the ball to select it; click again to deselect it. Use the arrows to move them. You can move many blocks at once. \nYou have 2 minutes!";
        }
        else
        {
            tutorialText.text =
                "To leave the room, you must move the ball to the opposite square. Click on a block or the ball to select it; click again to deselect it. Use the arrows to move them. You can move many blocks at once. Try to reach the pins to get additional time. \nYou have 2 minutes!";

        }
        
    }

    void OnMouseDown()
    {
        if (!tutorial.activeSelf)
        {
            isSelected = !isSelected; // Quando il blocco viene cliccato, diventa selezionato
            Rigidbody2D block = this.GetComponent<Rigidbody2D>();
            
            if (isSelected)
            {
                
               // Imposta il blocco come "cliccato" e non consente il clic su altri blocchi
                this.GetComponent<SpriteRenderer>().color = Color.red;
                
                block.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = Color.blue;
                block.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

            }
        }
      
    }

    

    // Update is called once per frame
    void Update()
    {
        

        if (isSelected && !tutorial.activeSelf)
        {
            movement.x=Input.GetAxisRaw("Horizontal");
            movement.y=Input.GetAxisRaw("Vertical");

            int speed = 35;
            
            Rigidbody2D block = gameObject.GetComponent<Rigidbody2D>();
            
            block.MovePosition(block.position + movement * speed * Time.deltaTime);
            
                
                // Ottieni il collider del blocco attuale
                Collider2D colliderAttuale = GetComponent<Collider2D>();

                // Ottieni tutti i collider degli altri blocchi
                Collider2D[] colliderAltriBlocchi = Physics2D.OverlapBoxAll(transform.position, colliderAttuale.bounds.size, 0);

                // Controlla se ci sono collisioni con altri blocchi
                foreach (Collider2D altroCollider in colliderAltriBlocchi)
                {
                
                    if(altroCollider.Equals(finishBlock))
                    {
                        textToShow.text = "YOU WIN!";
                        textToShow.gameObject.SetActive(true);
                        goBack.SetActive(true);
                        win = true;
                        timeRemaining = 0f;
                        break;
                    }
                
                    if (data.getPlayerCluster() == 0 && 
                        altroCollider.tag.Equals("Checkpoint"))
                    {
                        timeRemaining += 60f;
                        textToShow.text = "Checkpoint!";
                        textToShow.gameObject.SetActive(true);
                        altroCollider.gameObject.SetActive(false);
                    }
                
                    
                }
                
                
            
        }

        if (!tutorial.activeSelf)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                if (!win)
                {
                    tryAgain.SetActive(true);
                    gameObject.SetActive(false);
                    textToShow.text = "TRY AGAIN!";
                    textToShow.gameObject.SetActive(true);
                    timeRemaining = 0f;
                }
            }
            timeT.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
        }
        
        
    }
    
}
