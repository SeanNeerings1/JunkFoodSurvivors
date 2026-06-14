using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCooldownUI : MonoBehaviour
{
    public TMP_Text cooldownText;
    public Image abilityBox;

    [Range(0f, 1f)]
    public float cooldownOpacity = 0.45f;

    private float cooldownEndTime;
    private bool coolingDown;

    void Start()
    {
        ShowReady();
    }

    void Update()
    {
        if (!coolingDown) return;

        float timeLeft = cooldownEndTime - Time.time;

        if (timeLeft > 0)
        {
            cooldownText.text = Mathf.CeilToInt(timeLeft).ToString();
        }
        else
        {
            coolingDown = false;
            ShowReady();
        }
    }

    public void StartCooldown(float cooldownTime)
    {
        coolingDown = true;
        cooldownEndTime = Time.time + cooldownTime;

        if (abilityBox != null)
        {
            Color c = abilityBox.color;
            c.a = cooldownOpacity;
            abilityBox.color = c;
        }
    }

    void ShowReady()
    {
        if (cooldownText != null)
            cooldownText.text = "B";

        if (abilityBox != null)
        {
            Color c = abilityBox.color;
            c.a = 1f;
            abilityBox.color = c;
        }
    }
}