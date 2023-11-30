using UnityEngine;

public class CountNonAtt : MonoBehaviour
{
    public TMPro.TextMeshProUGUI countT, timeT;
    private ScoreManagerNonAttSite _scoreManagerNonAttSite = ScoreManagerNonAttSite.getInstance();
    public GameObject good;
    public GameObject goback;
    
    private float startingTime = 60f;
    private float timeRemaining;

    public void onSelectObject()
    {
        _scoreManagerNonAttSite.addSelectedObject(gameObject.name);
    }

    private void Start()
    {
        timeRemaining = startingTime;
        countT.text = countT.text = $"{_scoreManagerNonAttSite.getInitialScore()}";
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        countT.text = countT.text = $"{_scoreManagerNonAttSite.getActualScore()}";
        if (timeRemaining <= 0) {
            good.SetActive(true);
            goback.SetActive(true);
        }
            
        timeT.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
        if (_scoreManagerNonAttSite.getActualScore() == 0)
        {
            good.SetActive(true);
            goback.SetActive(true);
        }
    }
}
