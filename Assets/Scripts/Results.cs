using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Questions questions1;
    public Questions questions2;
    public Questions questions3;
    private Questions actualQuestions;
    public GameObject correctSprite;
    public GameObject incorrectSprite;

    public Scores scores;
    
    public List<Button> possibleanswers;

    public UnityEvent onNextQuestion;
    private static GameData gameDataInstance;
    void Start()
    {
        gameDataInstance = GameData.getInstance();
        correctSprite.SetActive(false);
        incorrectSprite.SetActive(false);
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
    }

    public void ShowResults(TMP_Text answer)
    {
        correctSprite.SetActive(actualQuestions.questionList[actualQuestions.currentQuestion].answer == answer.text);
        incorrectSprite.SetActive(actualQuestions.questionList[actualQuestions.currentQuestion].answer != answer.text);

        if (actualQuestions.questionList[actualQuestions.currentQuestion].answer == answer.text)
        {
            scores.AddScore();
            actualQuestions.questionList[actualQuestions.currentQuestion].questioned = true;
        }
        else
        {
            scores.DeductScore();
            actualQuestions.questionList[actualQuestions.currentQuestion].questioned = false;
        }

        for (var i = 0; i < 4; i++)
        {
            possibleanswers[i].interactable = false;
        }

        StartCoroutine(ShowResult());
    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(1.0f);
        
        correctSprite.SetActive(false);
        incorrectSprite.SetActive(false);

        for (var i = 0; i < 4; i++)
        {
            possibleanswers[i].interactable = true;
        }
        
        onNextQuestion.Invoke();
    }
}