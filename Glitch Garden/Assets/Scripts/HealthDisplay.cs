using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] int baseHealth = 5;
    [SerializeField] int damagePoints = 1;
    Text baseHealthText;

    void Start()
    {
        baseHealthText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (baseHealth <= 0)
        {
            baseHealth = 0;
        }
        baseHealthText.text = baseHealth.ToString();
    }

    public void TakenDamage()
    {
        damagePoints *= PlayerPrefsController.GetDifficulty();
        baseHealth -= damagePoints;
        UpdateDisplay();
        if (baseHealth <= 0)
        {
            FindObjectOfType<LevelController>().HandleLoseConditions();
            
        }
    }


}
