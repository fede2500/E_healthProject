using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Singleton
public class ScoreManager
{
    private static ScoreManager _scoreManagerInstance;
    private Dictionary<string, int> objectsAmount = new Dictionary<string, int>();
    private string selectedMedicine = null;
    
    private ScoreManager() {} 
 
    public static ScoreManager getInstance() {
        // Crea l'oggetto solo se NON esiste:
        if (_scoreManagerInstance == null) {
            _scoreManagerInstance = new ScoreManager();
        }
        return _scoreManagerInstance;
    }

    public string getSelectedMedicine()
    {
        return selectedMedicine;
    }

    public void setSelectedMedicine(string medicineName)
    {
        selectedMedicine = medicineName;
    }
    

    // Funzione per ottenere la quantità di un oggetto
    public int getObjectAmount(string name)
    {
        if (objectsAmount.ContainsKey(name))
        {
            return objectsAmount[name];
        }
        return 0;
    }
    public int getSelectedMedicineAmount()
    {
        if (selectedMedicine == null) return 0;
        if (objectsAmount.ContainsKey(selectedMedicine))
        {
            return objectsAmount[selectedMedicine];
        }
        return 0;
    }

    // Funzione per impostare la quantità di un oggetto
    public void setObjectAmount(string name, int amount)
    {
        if (objectsAmount.ContainsKey(name))
        {
            objectsAmount[name] = Mathf.Max(amount, 0); // Assicurati che la quantità sia almeno 0
        }
        else
        {
            objectsAmount.Add(name, Mathf.Max(amount, 0));
        }
    }

    public void decreaseSelectedMedicineAmount()
    {
        if (objectsAmount.ContainsKey(selectedMedicine))
        {
            objectsAmount[selectedMedicine] = Mathf.Max(getSelectedMedicineAmount()-1, 0);
        }
        else
        {
            objectsAmount.Add(selectedMedicine, 0);
        }
    }
    public void incrementSelectedMedicineAmount()
    {
        if (objectsAmount.ContainsKey(selectedMedicine))
        {
            objectsAmount[selectedMedicine] = Mathf.Min(getSelectedMedicineAmount()+1, 10);
        }
        else
        {
            objectsAmount.Add(selectedMedicine, 1);
        }
    }
}
