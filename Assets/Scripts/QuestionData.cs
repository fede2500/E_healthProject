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
    public Questions questions1;
    public Questions questions2;
    public Questions questions3;
    private Questions actualQuestions;
    public Scores scores;

    [SerializeField]
    public TMP_Text _questionText;

    public List<TMP_Text> answers;
    
    private static GameData gameDataInstance;
    
    void Start()
    {
        gameDataInstance = GameData.getInstance();
        gameDataInstance.setPlayerCluster(1);
        switch (gameDataInstance.getPlayerCluster())
        {
            case 0:
                actualQuestions = questions1;
                break;
            case 1:
                actualQuestions = questions2;
                break;
            case 2:
                actualQuestions = questions3; 
                break;
                    
        }
        AskQuestion();
    }

    public void AskQuestion()
    {
        if(CountValidQuestions() == 0)
        {
            _questionText.text = string.Empty;
            ClearQuestions();
            SceneManager.LoadScene("Menu");
            return;
        }

        var randomIndex = 0;
        do
        {
            var scoreasint =  int.Parse(scores.scoreText.text);
            randomIndex = UnityEngine.Random.Range(0, actualQuestions.questionList.Count);
            
        } while (actualQuestions.questionList[randomIndex].questioned == true);

        actualQuestions.currentQuestion = randomIndex;
        _questionText.text = actualQuestions.questionList[actualQuestions.currentQuestion].question;
        
        var randomanswer = UnityEngine.Random.Range(0, 3);

        List<int> random = new List<int>(6);
        var temp = 0;
        for (var i = 0; i < 4; i++)
        {
            if (i != randomanswer)
            {
                do
                {
                   temp = UnityEngine.Random.Range(0, actualQuestions.questionList.Count());
                } while (random.Contains(temp));

                random.Insert(i, temp);
            }
            else
            {
                random.Insert(i, randomIndex);
            }
        }
        answers[0].text = actualQuestions.questionList[random[0]].answer;
        answers[1].text = actualQuestions.questionList[random[1]].answer;
        answers[2].text = actualQuestions.questionList[random[2]].answer;
        answers[3].text = actualQuestions.questionList[random[3]].answer;
    }

    public void ClearQuestions()
    {
        foreach (var question in actualQuestions.questionList)
        {
            question.questioned = false;
        }
    }
    private int CountValidQuestions()
    {
        int validQuestion = 0;

        foreach (var question in actualQuestions.questionList)
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