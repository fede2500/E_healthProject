using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class PersonasQ : ScriptableObject
{
    [System.Serializable]
    public class ClusteringQuestions
    {
        public string question = string.Empty;
        public List<string> answers = new List<string>(3);
        public bool questioned = false;
      
    }

    public int currentQuestion = 0;
    
    public List<ClusteringQuestions> questionList;

    public void AddQuestion()
    {
        questionList.Add(new ClusteringQuestions());

    }

    
}
