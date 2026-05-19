using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerXp : MonoBehaviour
{
    [Header("experience")]
    [SerializeField] AnimationCurve experienceCurve;
    
    int currentLevel, totalexperience;
    int previousLevelExperience, nextLevelExperience;

    [Header("interface")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experiencefill;


    void Start()
    {
        UpdateLevel();   
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddExperience(5);
        }   
    }
    public void AddExperience(int amount)
    {
        totalexperience += amount;
        CheckForLevelUp();
        UpdateInterface();
    }

    void CheckForLevelUp()
    {
        while (totalexperience >= nextLevelExperience)
        {
            currentLevel++;
            UpdateLevel();
        }
    }
    void UpdateLevel()
    {
        previousLevelExperience = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelExperience = (int)experienceCurve.Evaluate(currentLevel + 1);
        UpdateInterface();
    }
    void UpdateInterface()
    {
        int start = totalexperience - previousLevelExperience;
        int end = nextLevelExperience - previousLevelExperience;

        levelText.text = currentLevel.ToString();
        experienceText.text = start + "exp /" + end + " exp";
        experiencefill.fillAmount = (float)start / (float)end;
    }
}
