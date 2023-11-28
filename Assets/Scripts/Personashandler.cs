using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Personashandler : MonoBehaviour
{
    public  PersonasQ questions;

    [SerializeField]
    public TMP_Text _questionText;
    public List<TMP_Text> possibleanswers = new List<TMP_Text>(3);
    private static GameData gameDataInstance;
    //private Clusterization clus;
    
    void Start()
    {
        AskQuestion();
        gameDataInstance = GameData.getInstance();
    }

    public void AskQuestion()
    {
        if(CountValidQuestions() == 0)
        {
            _questionText.text = string.Empty;
            questions.currentQuestion = 0;
            ClearQuestions();
            //clus.CalculatingClusters();
            SceneManager.LoadScene("Room");
            return;
        }

        
        _questionText.text = questions.questionList[questions.currentQuestion].question;
        possibleanswers[0].text = questions.questionList[questions.currentQuestion].answers[0];
        possibleanswers[1].text = questions.questionList[questions.currentQuestion].answers[1];
        possibleanswers[2].text = questions.questionList[questions.currentQuestion].answers[2];
  
    }

    public void ClearQuestions()
    {
        foreach (var question in questions.questionList)
        {
            question.questioned = false;
        }
    }
    private int CountValidQuestions()
    {
        int validQuestion = 0;

        foreach (var question in questions.questionList)
        {
            if (question.questioned == false)
            {
                validQuestion++;
            }
        }
        Debug.Log("Questions Left");
        return validQuestion;
        
    }
}
