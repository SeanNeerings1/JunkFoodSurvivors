using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehavior : EnemyBehavior
{
    [Header("Boss Defeat Settings")]
    public string bossName = "The doughminator";

    [Header("Boss Death Audio")]
    public AudioClip bossDeathSound; 
    [Range(0f, 1f)] public float deathSoundVolume = 1.0f;
    protected override void Die()
    {
        Debug.Log($"{bossName} is defeated!");
        if (bossDeathSound != null)
        {
            AudioSource.PlayClipAtPoint(bossDeathSound, transform.position, deathSoundVolume);
        }
        if (KillMeter.Instance != null)
        {
            KillMeter.Instance.AddKill();
        }
        else
        {
            Debug.LogWarning("KillMeter is not found!");
        }
        SceneManager.LoadScene("WinScene");
        Destroy(gameObject);
    }
}