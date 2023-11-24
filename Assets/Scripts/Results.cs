using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Questions questions;
    public GameObject correctSprite;
    public GameObject incorrectSprite;

    public Scores scores;
    
    public List<Button> possibleanswers;

    public UnityEvent onNextQuestion;
    void Start()
    {
        correctSprite.SetActive(false);
        incorrectSprite.SetActive(false);
    }

    public void ShowResults(TMP_Text answer)
    {
        correctSprite.SetActive(questions.questionList[questions.currentQuestion].answer == answer.text);
        incorrectSprite.SetActive(questions.questionList[questions.currentQuestion].answer != answer.text);

        if (questions.questionList[questions.currentQuestion].answer == answer.text)
        {
            scores.AddScore();
            questions.questionList[questions.currentQuestion].questioned = true;
        }
        else
        {
            scores.DeductScore();
            questions.questionList[questions.currentQuestion].questioned = false;
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