using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountNonAtt : MonoBehaviour
{
    public TMPro.TextMeshProUGUI countT, timeT;
    private ScoreManagerNonAttSite _scoreManagerNonAttSite=ScoreManagerNonAttSite.getInstance();
    public GameObject time;
    public GameObject good;
    public GameObject goback;
    public GameObject tryagain;
    public GameObject hint;
    private bool playgame;
    
    private float startingTime = 30f;
    private float timeRemaining;

    private GameData data = GameData.getInstance();

    public void onSelectObject()
    {
        _scoreManagerNonAttSite.addSelectedObject(gameObject.name);
    }

    private void Start()
    {
        
        playgame = true;
        timeRemaining = startingTime;
        countT.text = countT.text = $"{_scoreManagerNonAttSite.getInitialScore()}";
        switch(data.getPlayerCluster())
        {
            case 1:
                hint.SetActive(true);
                break;
            default:
                time.SetActive(true);
                break;

        }
        
    }
    
    
    private void Update()
    {
        switch(data.getPlayerCluster())
        {
            case 1:
                break;
            default:
                if (playgame)
                {
                    timeRemaining -= Time.deltaTime;
                }

                
                if (timeRemaining <= 0)
                {

                    playgame = false;
                    tryagain.SetActive(true);
                    GameObject butt1 = GameObject.Find("warning");
                    GameObject butt2 = GameObject.Find("banner");
                    GameObject butt3 = GameObject.Find("selling");
                    GameObject butt4 = GameObject.Find("Weird_explaination");
                    GameObject butt5 = GameObject.Find("selling_2");

                    butt1.SetActive(false);
                    butt2.SetActive(false);
                    butt3.SetActive(false);
                    butt4.SetActive(false);
                    butt5.SetActive(false);

                }

                timeT.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
                break;

        }
        
        countT.text = countT.text = $"{_scoreManagerNonAttSite.getActualScore()}";
        
        
        if (_scoreManagerNonAttSite.getActualScore() == 0)
        {
            playgame = false;
            good.SetActive(true);
            goback.SetActive(true);
        }
    }

    public void restartGame()
    {
        _scoreManagerNonAttSite.restart();
        SceneManager.LoadScene("Non_att_site");
    }
}
