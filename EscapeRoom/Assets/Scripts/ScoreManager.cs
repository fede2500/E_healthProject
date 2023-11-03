using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public List<Mole> moles; 
    public GameObject playButton;
    public GameObject gameUI;
    public GameObject gameOver;
    public TMPro.TextMeshProUGUI scoreT;
    public TMPro.TextMeshProUGUI timeT;

    private float startingTime = 60f;

    private float timeRemaining;
    
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    private int score;
    private bool playing = false;

    public void StartGame()
    {
        playButton.SetActive(false);
        gameOver.SetActive(false);
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
            if (currentMoles.Count <= (score/10)) {
                // Choose a random mole.
                int index = Random.Range(0, moles.Count);
                // Doesn't matter if it's already doing something, we'll just try again next frame.
                if (!currentMoles.Contains(moles[index])) {
                    currentMoles.Add(moles[index]);
                    moles[index].Activate(score/10);
                }
            }
            
            
        }
    }
    
    public void GameOver() {
        // Stop the game and show the start UI.
        playing = false;
        gameOver.SetActive(true);
    }



    public void AddScore(int moleIndex) {
        // Add and update score.
        score += 1;
        scoreT.text = $"{score}";
        
        // Remove from active moles.
        currentMoles.Remove(moles[moleIndex]);
    }
    
    public void RemoveScore(int moleIndex) {
        // Add and update score.
        score -= 0;
        if (score < 0)
        {
            score = 0;
        }
        
        scoreT.text = $"{score}";
        
        // Remove from active moles.
        currentMoles.Remove(moles[moleIndex]);
    }
    
}
