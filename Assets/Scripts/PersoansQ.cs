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
    // public List<int> freq = new List<int>(3);

    
    public List<ClusteringQuestions> questionList;

    public void AddQuestion()
    {
        questionList.Add(new ClusteringQuestions());
    }

    // public void initialization(List<int> freq)
    // {
    //     for(var i = 0; i < 3; i++)
    //     {
    //         freq[i] = 0;
    //     }
    // }
}
