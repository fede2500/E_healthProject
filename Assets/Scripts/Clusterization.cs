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

    public Clusterization() {} 
    
public void CalculatingFrequency(TMP_Text answer)
    {
        
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
        questions.questionList[questions.currentQuestion].questioned = true;
        questions.currentQuestion++;
        
        CalculatingClusters();
        
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

    public void CalculatingClusters()
    {
        gameDataInstance = GameData.getInstance();
        int age = gameDataInstance.getPlayerAge();

        double clust = 0;
        if (age < 32)
        {
            clust = 0;
        }
        else
        {
            clust = 1.5;
        }

        clust = clust + freq[0] * 0 + freq[1] * 1 + freq[2] * 2;


        if (clust <= 3.5)
        {
            gameDataInstance.setPlayerCluster(0);
            Debug.Log($"{gameDataInstance.getPlayerCluster()}");
        }
        else if (clust > 3.5 && clust < 7.5)
        {
            gameDataInstance.setPlayerCluster(1);
            Debug.Log($"{gameDataInstance.getPlayerCluster()}");
        }
        else if (clust >=7.5)
        {
            gameDataInstance.setPlayerCluster(2);
            Debug.Log($"{gameDataInstance.getPlayerCluster()}");
        }

    }
}


