using UnityEngine;

public class BlockMovements : MonoBehaviour
{
    public Collider2D finishBlock;
    private bool isSelected = false;
    public GameObject tutorial;
    
    
    private Vector2 movement;

    
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

            int speed = 25;
            
            Rigidbody2D block = gameObject.GetComponent<Rigidbody2D>();
            
            block.MovePosition(block.position + movement * speed * Time.deltaTime);
            
        }
    }
    
}
