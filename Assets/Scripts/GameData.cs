using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    private static GameData gameDataInstance;
    private string currentObjectMinigame;
    private Dictionary<string, bool> minigame_played_status = new Dictionary<string, bool>();
    public Vector2 player = Vector2.zero;
    private bool button;
    private bool computerPlayed;

    private int cluster;
    
    private GameData() {} 
    
    public static GameData getInstance() {
        // Crea l'oggetto solo se NON esiste:
        if (gameDataInstance == null) {
            gameDataInstance = new GameData();
        }
        return gameDataInstance;
    }
    
    public void setCluster(int cluster)
    {
        this.cluster = cluster;
    }

    public int getCluster()
    {
        return cluster;
    }

    public Vector2 getPlayer()
    {
        return player;
    }

    public void setPlayer(Vector2 playerSet)
    {
        player = playerSet;
    }

    public bool isNoMinigamePlayed()
    {
        if (minigame_played_status.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isMinigamePlayed(string gameObjectName)
    {
        if (minigame_played_status.ContainsKey(gameObjectName))
        {
            return minigame_played_status[gameObjectName];
        }
        return false;
    }

    public void setCurrentObjectMinigame(string currentObjectName)
    {
        currentObjectMinigame = currentObjectName;
    }

    public void setMinigamePlayed()
    {
        if (minigame_played_status.ContainsKey(currentObjectMinigame))
        {
            minigame_played_status[currentObjectMinigame] = true; // Assicurati che la quantità sia almeno 0
        }
        else
        {
            minigame_played_status.Add(currentObjectMinigame, true);
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
