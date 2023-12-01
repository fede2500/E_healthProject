using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    private string playerName;
    private int playerCluster;
    private int playerAge;
    
    private static GameData gameDataInstance;
    private string currentObjectMinigame;
    private string precObjMinigame;
    private Dictionary<string, bool> minigame_played_status = new Dictionary<string, bool>();
    public Vector2 player = Vector2.zero;
    private int moleplayed = 0;
    

    private List<string> gameOrder = new List<string>()
    {
        "Computer",
        "TV",
        "Locker",
        "Bookshelf"
    };
    
    private GameData() {} 
    
    public static GameData getInstance() {
        // Crea l'oggetto solo se NON esiste:
        if (gameDataInstance == null) {
            gameDataInstance = new GameData();
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
            minigame_played_status[currentObjectMinigame] = true;
            
        }
        else
        {
            minigame_played_status.Add(currentObjectMinigame, true);
        }

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
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
