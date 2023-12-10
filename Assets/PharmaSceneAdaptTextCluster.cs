using UnityEngine;
using TMPro;

public class PharmaSceneAdaptTextCluster : MonoBehaviour
{
    public TextMeshProUGUI textToShow;
    private GameData data = GameData.getInstance();
    // Start is called before the first frame update
    void Start()
    {
        
        switch (data.getPlayerCluster())
        {
            case 1 :
                textToShow.text = "Take 70 mg of creedoxin twice a day, <u>after meals</u>. Thereafter, take 85 mg of pokevitamin twice a day <u>before meals</u>. Continue by taking 50 mg of  linkazol, 30 minutes <u>before dinner</u>. Finally, take 50 mg of laytonium every day, 30 minutes <u>before a meal.</u>";
            break;
            
            default:
                textToShow.text = "Take 70 mg of creedoxin twice a day, after meals. Thereafter, take 85 mg of pokevitamin twice a day before meals. Continue by taking 50 mg of  linkazol, 30 minutes before dinner. Finally, take 50 mg of laytonium every day, 30 minutes before a meal.";
            break;
                
        }
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
