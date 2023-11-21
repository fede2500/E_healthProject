
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public bool hittable = false;
    private void OnMouseDown()
    {
        Debug.Log("ciao");
        ChangeSceneWithDelay("TrueFake");
    }
    
    // Metodo chiamato quando il pulsante viene premuto
    public void ChangeSceneWithDelay(string sceneName)
    {
        // Avvia la coroutine per attendere due secondi
        StartCoroutine(WaitAndLoadScene(sceneName));
    }

    // Coroutine per attendere due secondi e quindi cambiare la scena
    private IEnumerator WaitAndLoadScene(string sceneName)
    {
        // Attendi due secondi
        yield return new WaitForSeconds(0);

        // Carica la scena specificata
        if (hittable)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}