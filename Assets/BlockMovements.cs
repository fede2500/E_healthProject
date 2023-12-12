using UnityEngine;

public class BlockMovements : MonoBehaviour
{
    public Collider2D finishBlock;
    private bool isSelected = false;
    public GameObject tutorial;
    

    void OnMouseDown()
    {
        
        if (!tutorial.activeSelf)
        {
            isSelected = !isSelected; // Quando il blocco viene cliccato, diventa selezionato
        
            if (isSelected)
            {
                // Imposta il blocco come "cliccato" e non consente il clic su altri blocchi
                this.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
        
       
    }

    

    // Update is called once per frame
    void Update()
    {

        if (isSelected && !tutorial.activeSelf)
        {
            // Salva la posizione attuale prima del movimento
            Vector2 posizioneAttuale = transform.position;
            
            // Controlla l'input per il movimento verso destra
            if (Input.GetKeyDown(KeyCode.RightArrow))  // Cambia KeyCode in base alla tua esigenza
            {
                // Sposta il blocco a destra
                transform.Translate(Vector2.right * 60 * Time.deltaTime);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))  // Cambia KeyCode in base alla tua esigenza
            {
                // Sposta il blocco a destra
                transform.Translate(Vector2.left * 60 * Time.deltaTime);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))  // Cambia KeyCode in base alla tua esigenza
            {
                // Sposta il blocco a destra
                transform.Translate(Vector2.up * 60 * Time.deltaTime);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))  // Cambia KeyCode in base alla tua esigenza
            {
                // Sposta il blocco a destra
                transform.Translate(Vector2.down * 60 * Time.deltaTime);
            } 
            
            

            // Ottieni il collider del blocco attuale
            Collider2D colliderAttuale = GetComponent<Collider2D>();

            // Ottieni tutti i collider degli altri blocchi
            Collider2D[] colliderAltriBlocchi = Physics2D.OverlapBoxAll(transform.position, colliderAttuale.bounds.size, 0);

            // Controlla se ci sono collisioni con altri blocchi
            foreach (Collider2D altroCollider in colliderAltriBlocchi)
            {
                
                if (altroCollider != colliderAttuale && 
                    !altroCollider.Equals(finishBlock) &&
                    !altroCollider.tag.Equals("Checkpoint"))
                {
                    // Se c'è sovrapposizione, annulla il movimento
                    transform.position = posizioneAttuale;
                    break;
                }
            }
        }
    }
    
}
