using UnityEngine;
using TMPro; 

public class KillMeter : MonoBehaviour
{ //makes it accesable every where
    public static KillMeter Instance;

    public TextMeshProUGUI killText;
    private int killCount = 0;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    private void Start()
    {
        UpdateUI();
    }
    public void AddKill()
    {
        killCount++;
        UpdateUI();
    }

    void UpdateUI()
    {
        killText.text = ": " + killCount;
    }
}