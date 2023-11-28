using UnityEngine;

public class CountNonAtt : MonoBehaviour
{
    public TMPro.TextMeshProUGUI countT;
    private ScoreManagerNonAttSite _scoreManagerNonAttSite = ScoreManagerNonAttSite.getInstance();
    public GameObject good;
    public GameObject goback;

    public void onSelectObject()
    {
        _scoreManagerNonAttSite.addSelectedObject(gameObject.name);
    }

    private void Start()
    {
        countT.text = countT.text = $"{_scoreManagerNonAttSite.getInitialScore()}";
    }

    private void Update()
    {
        countT.text = countT.text = $"{_scoreManagerNonAttSite.getActualScore()}";
        if (_scoreManagerNonAttSite.getActualScore() == 0)
        {
            good.SetActive(true);
            goback.SetActive(true);
        }
    }
}
