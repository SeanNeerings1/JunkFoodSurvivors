using UnityEngine;
using TMPro;

public class GameOverTime : MonoBehaviour
{
    public TextMeshProUGUI finalTimeText;

    void Start()
    {
        float finalTime = PlayerPrefs.GetFloat("FinalTime", 0f);

        int minutes = Mathf.FloorToInt(finalTime / 60f);
        int seconds = Mathf.FloorToInt(finalTime % 60f);

        if (finalTimeText != null)
        {
            finalTimeText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}