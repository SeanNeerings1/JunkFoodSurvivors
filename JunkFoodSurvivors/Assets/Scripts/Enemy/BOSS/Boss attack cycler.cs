using UnityEngine;

public class BossAttackCycler : MonoBehaviour
{
    [Header("Scripts to Cycle Through")]
    // a array so we can put in as much attacks as we want
    public BossShoot[] shootScripts;

    [Header("Audio Settings")]
    [Tooltip("Audio source boss here")]
    public AudioSource bossAudioSource;
    [Tooltip("makes sure the order is the same as the scripts")]
    public AudioClip[] attackVoiceLines;

    [Header("Timing Settings")]
    public float switchInterval = 7f; // seconds before he switches
    private float _switchTimer;
    private int _currentScriptIndex = 0;
    void Start()
    {
        _switchTimer = switchInterval;
        // controls if scripts are in list
        if (shootScripts == null || shootScripts.Length == 0)
        {
            Debug.LogWarning("attack script not found");
            return;
        }
        if (bossAudioSource == null)
        {
            bossAudioSource = GetComponent<AudioSource>();
        }

        // turns of all scripts except the first
        for (int i = 0; i < shootScripts.Length; i++)
        {
            if (shootScripts[i] != null)
            {
                shootScripts[i].enabled = (i == 0);
            }
        }
        PlayVoiceLine(0);
    }
    void Update()
    {
        if (shootScripts == null || shootScripts.Length <= 1) return;
        // counts the time
        _switchTimer -= Time.deltaTime;
        if (_switchTimer <= 0f)
        {
            // done so switch
            NextAttackPattern();
            // resets time
            _switchTimer = switchInterval;
        }
    }
    void NextAttackPattern()
    {
        // 1.turns of current attack script
        if (shootScripts[_currentScriptIndex] != null)
        {
            shootScripts[_currentScriptIndex].enabled = false;
        }
        // 2.go to next in list
        _currentScriptIndex++;
        // if we at the end restart
        if (_currentScriptIndex >= shootScripts.Length)
        {
            _currentScriptIndex = 0;
        }
        // 3. turns on new script
        if (shootScripts[_currentScriptIndex] != null)
        {
            shootScripts[_currentScriptIndex].enabled = true;
        }
        PlayVoiceLine(_currentScriptIndex);
    }
    void PlayVoiceLine(int index)
    {
        if (bossAudioSource != null && attackVoiceLines != null && index < attackVoiceLines.Length)
        {
            if (attackVoiceLines[index] != null)
            {
                bossAudioSource.clip = attackVoiceLines[index];
                bossAudioSource.Play();
            }
        }
    }
}
