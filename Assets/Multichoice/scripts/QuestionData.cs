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

public class QuestionData : MonoBehaviour
{
    public Questions questions;
    public Scores scores;

    [SerializeField]
    public TMP_Text _questionText;

    public List<TMP_Text> answers;
    
    void Start()
    {
        AskQuestion();
    }

    public void AskQuestion()
    {
        if(CountValidQuestions() == 0)
        {
            _questionText.text = string.Empty;
            ClearQuestions();
            SceneManager.LoadScene("MainMenu");
            return;
        }

        var randomIndex = 0;
        do
        {
            var scoreasint = int.Parse(scores.scoreText.text);
            if (scoreasint < 50)
            {
                randomIndex = UnityEngine.Random.Range(0, 4);
            }
            else if (scoreasint >= 50 && scoreasint < 100)
            {
                randomIndex = UnityEngine.Random.Range(20, 39);
            }
            else
            {
                randomIndex = UnityEngine.Random.Range(40, questions.questionList.Count);
            }
            
        } while (questions.questionList[randomIndex].questioned == true);

        questions.currentQuestion = randomIndex;
        _questionText.text = questions.questionList[questions.currentQuestion].question;
        
        var randomanswer = UnityEngine.Random.Range(0, 3);

        List<int> random = new List<int>(6);
        var temp = 0;
        for (var i = 0; i < 4; i++)
        {
            if (i != randomanswer)
            {
                do
                {
                   temp = UnityEngine.Random.Range(0, questions.questionList.Count());
                } while (random.Contains(temp));

                random.Insert(i, temp);
            }
            else
            {
                random.Insert(i, randomIndex);
            }
        }
        answers[0].text = questions.questionList[random[0]].answer;
        answers[1].text = questions.questionList[random[1]].answer;
        answers[2].text = questions.questionList[random[2]].answer;
        answers[3].text = questions.questionList[random[3]].answer;
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