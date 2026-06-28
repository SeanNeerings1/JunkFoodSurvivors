using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerXp : MonoBehaviour
{
    private int currentLevel;
    private int totalExperience;
    private int previousLevelExperience;
    private int nextLevelExperience;

    public int CurrentLevel
    {
        get { return currentLevel; }
    }

    [Header("Interface")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private Image experienceFill;

    private void Start()
    {
        UpdateLevel();
        UpdateInterface();
    }

    private void Update()
    {
      
    }

    public void AddExperience(int amount)
    {
        totalExperience += amount;

        Debug.Log("XP Added: " + amount);
        Debug.Log("Total XP: " + totalExperience);

        CheckForLevelUp();
        UpdateInterface();
    }

    private void CheckForLevelUp()
    {
        int safety = 0;

        while (totalExperience >= nextLevelExperience && safety < 100)
        {
            currentLevel++;
            UpdateLevel();

            Debug.Log("LEVEL UP! Level " + currentLevel);

            safety++;
        }
    }

    private void UpdateLevel()
    {
        previousLevelExperience = GetRequiredXPForLevel(currentLevel);
        nextLevelExperience = GetRequiredXPForLevel(currentLevel + 1);
    }

    private int GetRequiredXPForLevel(int level)
    {
        return level * level * 100;
    }

    private void UpdateInterface()
    {
        int currentLevelXP = totalExperience - previousLevelExperience;
        int requiredXP = nextLevelExperience - previousLevelExperience;

        if (levelText != null)
            levelText.text = currentLevel.ToString();

        if (experienceText != null)
            experienceText.text = currentLevelXP + " XP / " + requiredXP + " XP";

        if (experienceFill != null)
        {
            experienceFill.fillAmount = requiredXP > 0
                ? (float)currentLevelXP / requiredXP
                : 0f;
        }
    }
}