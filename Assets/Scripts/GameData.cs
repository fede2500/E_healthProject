using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField]
    private string playerName;
    [SerializeField]
    private int playerCluster;
    private int playerAge;
    
    private static GameData gameDataInstance;
    
    [SerializeField]
    private string currentObjectMinigame;
    [SerializeField]
    public string precObjMinigame = null;
    
    public Vector2 player = Vector2.zero;
    [SerializeField]
    private int moleplayed = 0;
    [SerializeField]
    private bool quizplayed = false;
    

    private List<string> gameOrder = new List<string>()
    {
        "Computer",
        "TV",
        "Locker",
        "Bookshelf",
        "Door"
    };
    
    private GameData() {} 
    
    public static GameData getInstance() {
        // Crea l'oggetto solo se NON esiste:
        if (gameDataInstance == null) {
            gameDataInstance = new GameData();
            gameDataInstance.precObjMinigame = null;
        }
        return gameDataInstance;
    }

    public int getPlayerAge()
    {
        return playerAge;
    }

    public void setPlayerAge(int age)
    {
        playerAge = age;
    }
    
    public string getPlayerName()
    {
        return playerName;
    }

    public void setPlayerName(string name)
    {
        playerName = name;
    }

    public string getPrecedentGameName(string callerGameObject)
    {
        if (callerGameObject.Equals(gameOrder[0]))
        {
            return callerGameObject;
        }
        else
        {
            return gameOrder[gameOrder.IndexOf(callerGameObject) - 1];
        }
        
    }

    public string getFirstGameName()
    {
        return gameOrder[0];
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
        if (precObjMinigame == null)
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
        if(gameOrder.IndexOf(gameObjectName) <= gameOrder.IndexOf(currentObjectMinigame)  )
        {
            return true;
        }
        return false;
    }

    public void setCurrentObjectMinigame(string currentObjectName)
    {
        currentObjectMinigame = currentObjectName;
    }

    public void setMinigamePlayed()
    {
        precObjMinigame = currentObjectMinigame;
    }

    public string getPrecObjMinigame()
    {
        return precObjMinigame;
    }
    
    public void setPlayerCluster(int cluster)
    {
        playerCluster = cluster;

    }

    public int getPlayerCluster()
    {
        return playerCluster;
    }
    
    public void setMolePlayed(int mole)
    {
        moleplayed = mole;
    }
    
    public int getMolePlayed()
    {
        return moleplayed;
    }
    
    public void setQuizPlayed(bool quiz)
    {
        quizplayed = quiz;
    }
    public bool getQuizPlayed()
    {
        return quizplayed;
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
