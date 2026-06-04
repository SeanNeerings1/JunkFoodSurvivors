using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject bossPrefab; 
    public Transform spawnPoint;   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnTheBoss();
        }
    }

    void SpawnTheBoss()
    {
        if (bossPrefab == null)
        {
            Debug.LogWarning("BossSpawner:boss doesnt have a prefab!");
            return;
        }
        Vector3 spawnPosition = transform.position;
        if (spawnPoint != null)
        {
            spawnPosition = spawnPoint.position;
        }

        GameObject spawnedBoss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("BossSpawner: the boss has spawned");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SwitchToBossMusic();
        }
        else
        {
            Debug.LogWarning("BossSpawner: AudioManager not found");
        }
    }
}