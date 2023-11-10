using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    
    public TMPro.TextMeshProUGUI dialogueT;

    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen",true);
        Debug.Log("Starting conversation");
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNext();

    }

    public void DisplayNext()
    {
        if (sentences.Count() == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen",false);
        Debug.Log("Ending conversation");
    }

    private IEnumerator TypeSentence(string sentence)
    {
        yield return new WaitForSeconds(0.3f);
        dialogueT.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueT.text += letter;
            yield return null;
        }

    }
    

}
