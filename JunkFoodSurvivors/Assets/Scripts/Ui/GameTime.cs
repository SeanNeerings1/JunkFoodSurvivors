using UnityEngine;
using TMPro;

public class GameTime : MonoBehaviour
{
    public static GameTime Instance { get; private set; }

    [Header("UI Reference")]
    public string textTagName = "TimerText";
    private TextMeshProUGUI timerText;

    private float timeSpent = 0f;
    private bool isTimerRunning = true;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FindActiveTimerText();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeSpent += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void OnDestroy()
    {
        SaveFinalTime();
    }

    public void SaveFinalTime()
    {
        PlayerPrefs.SetFloat("FinalTime", timeSpent);
        PlayerPrefs.Save();
    }

    private void FindActiveTimerText()
    {
        GameObject textObject = GameObject.FindWithTag(textTagName);
        if (textObject != null)
        {
            timerText = textObject.GetComponent<TextMeshProUGUI>();
            UpdateTimerDisplay();
        }
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void AssignNewTextElement(TextMeshProUGUI newTextElement)
    {
        timerText = newTextElement;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(timeSpent / 60f);
        int seconds = Mathf.FloorToInt(timeSpent % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}