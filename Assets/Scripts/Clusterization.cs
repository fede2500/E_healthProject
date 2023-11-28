using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Clusterization : MonoBehaviour
{
    public PersonasQ questions;
    
    public List<Button> possibleanswers;

    public UnityEvent onNextQuestion;


    public void CalculatingFrequency(TMP_Text answer)
    {
        // if(questions.questionList[questions.currentQuestion].answers[0] == answer.text)
        // {
        //     questions.freq[0]++;
        // }
        // else if(questions.questionList[questions.currentQuestion].answers[1] == answer.text)
        // {
        //     questions.freq[1]++;
        // }
        // else
        // {
        //     questions.freq[2]++;
        // }
        
        for (var i = 0; i < 3; i++)
        {
            possibleanswers[i].interactable = false;
        }

        StartCoroutine(CalculatingFrequencies());
    }

    private IEnumerator CalculatingFrequencies()
    {
        yield return new WaitForSeconds(1.0f);

        for (var i = 0; i < 3; i++)
        {
            possibleanswers[i].interactable = true;
        }
        
        onNextQuestion.Invoke();
    }
}

