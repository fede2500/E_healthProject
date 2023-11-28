using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Clusterization : MonoBehaviour
{
    public PersonasQ questions;

    public List<Button> possibleanswers;

    public UnityEvent onNextQuestion;

    private static GameData gameDataInstance;

    public int [] freq = new int[] {0, 0, 0};


public void CalculatingFrequency(TMP_Text answer)
    {
        gameDataInstance = GameData.getInstance();
        
        if(questions.questionList[questions.currentQuestion].answers[0] == answer.text)
        {
            freq[0]++;
        }
        else if(questions.questionList[questions.currentQuestion].answers[1] == answer.text)
        {
            freq[1]++;
        }
        else if(questions.questionList[questions.currentQuestion].answers[2] == answer.text)
        {
            freq[2]++;
        }
        
        for (var i = 0; i < 3; i++)
        {
            possibleanswers[i].interactable = false;
        }
        questions.currentQuestion++;
        questions.questionList[questions.currentQuestion].questioned = true;
        Debug.Log($"{freq[0]}");
        Debug.Log($"{freq[1]}");
        Debug.Log($"{freq[2]}");

        StartCoroutine(CalculatingFrequencies());
    }

    private IEnumerator CalculatingFrequencies()
    {
        yield return new WaitForSeconds(0.6f);

        for (var i = 0; i < 3; i++)
        {
            possibleanswers[i].interactable = true;
        }
        
        onNextQuestion.Invoke();
        
    }

    public void calculatingClusters()
    {
        
        
        
    }
}


