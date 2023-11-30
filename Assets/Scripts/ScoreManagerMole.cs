using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManagerMole : MonoBehaviour
{
    public List<Mole> moles; 
    public GameObject playButton;
    public GameObject gameUI;
    public GameObject gameOver;
    public GameObject goBack;
    public TMPro.TextMeshProUGUI scoreT;
    public TMPro.TextMeshProUGUI timeT;
    public Dialogue dialogue;

    private float startingTime = 60f;
    private float timeRemaining;
    
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    private int score=0;
    private bool playing = false;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        playButton.SetActive(true);
    }
    
    public void StartGame()
    {
        playButton.SetActive(false);
        gameOver.SetActive(false);
        goBack.SetActive(false);
        gameUI.SetActive(true);
        
        for (int i = 0; i < moles.Count; i++) {
            moles[i].QuickHide();
            moles[i].SetIndex(i);
        }
        
        currentMoles.Clear();
        // Start with 30 seconds.
        timeRemaining = startingTime;
        score = 0;
        playing = true;
        
        scoreT.text = $"{score}";
        timeT.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
    }
    
    void Update() {
        if (playing) {
            // Update time.
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0) {
                timeRemaining = 0;
                GameOver();
            }
            
            timeT.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
            
            // Check if we need to start any more moles.
            if (currentMoles.Count <= score/30) {
                // Choose a random mole.
                int index = Random.Range(0, 9);
                // Doesn't matter if it's already doing something, we'll just try again next frame.
                if (!currentMoles.Contains(moles[index])) {
                    currentMoles.Add(moles[index]);
                    if (timeRemaining < 30f)
                    {
                        moles[index].Activate((score/30)*5);
                    }
                    else
                    {
                        moles[index].Activate(score/30);
                    }
                }
            }
            
        }
    }

    public void Refresh()
    { 
        currentMoles.Clear();
    }
    
    public void GameOver() {
        GameData data = GameData.getInstance();
        // Stop the game and show the start UI.
        playing = false;

        if (score < 30)
        {
            switch (data.getPlayerCluster())
            {
                case 0:
                    dialogue.sentences = new[]
                    {
                        "Give it another go!",
                    };
                        break;
                case 1:
                    dialogue.sentences = new[]
                    {
                        "I’ve never seen someone score so low!! Come on try again!"
                    };
                    break;
                case 2:
                    dialogue.sentences = new[]
                    {
                       "I know you can do better, come on!"
                    };
                    break;
            }
            FindObjectOfType<DialogManager>().StartDialogue(dialogue);
        }
        else
        {
            gameOver.SetActive(true);
            goBack.SetActive(true);
        }
        
       
    }



    public void AddScore(int moleIndex) {
        // Add and update score.
        score += 3;
        scoreT.text = $"{score}";
        
        // Remove from active moles.
        currentMoles.Remove(moles[moleIndex]);
    }
    
    public void RemoveScore(int moleIndex) {
        // Add and update score.
        score -= 5;
        if (score < 0)
        {
            score = 0;
        }
        
        scoreT.text = $"{score}";
        
        // Remove from active moles.
        currentMoles.Remove(moles[moleIndex]);
    }
    
}
