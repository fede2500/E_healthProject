using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagerNonAttSite
{
    private static ScoreManagerNonAttSite _scoreManagerInstance;
    private static int NCHECKS = 5;
    
    private Dictionary<string, bool> fake_already_checked = new Dictionary<string, bool>();
    
    private ScoreManagerNonAttSite() {} 
 
    public static ScoreManagerNonAttSite getInstance() {
        // Crea l'oggetto solo se NON esiste:
        if (_scoreManagerInstance == null)
        {
            _scoreManagerInstance = new ScoreManagerNonAttSite();
        }
        return _scoreManagerInstance;
    }

    public void addSelectedObject(string selectedGameObject)
    {
        if (! fake_already_checked.ContainsKey(selectedGameObject))
        {
            fake_already_checked.Add(selectedGameObject, true);
            
        }
    }

    public int getInitialScore()
    {
        return NCHECKS;
    }

    public int getActualScore()
    {
        return NCHECKS - fake_already_checked.Count;
    }
}
