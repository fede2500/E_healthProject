using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Questions : ScriptableObject
{
    [System.Serializable]
    public class QuestionsData
    {
        public string question = string.Empty;
        public string answer = string.Empty;
        public bool questioned = false;
      
    }

    public int currentQuestion = 0;
    public List<QuestionsData> questionList;

    public void AddQuestion()
    {
        questionList.Add(new QuestionsData());
    }
}
