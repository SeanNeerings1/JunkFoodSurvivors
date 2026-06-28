using UnityEngine;

public class BombManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject bombPrefab;
    public float spawnCooldown = 5.0f;
    private float nextSpawnTime = 0f;

    public AbilityCooldownUI cooldownUI;
    public void UI_TriggerBomb()
    {
        if (Time.time >= nextSpawnTime)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && bombPrefab != null)
            {
                Instantiate(bombPrefab, player.transform.position, Quaternion.identity);
                nextSpawnTime = Time.time + spawnCooldown;
                if (cooldownUI != null)
                {
                    cooldownUI.StartCooldown(spawnCooldown);
                }
            }
            else if (player == null)
            {
                Debug.LogError("cant vind player tag");
            }
        }
    }
}