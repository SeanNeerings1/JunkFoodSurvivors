using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject bossPrefab; 
    public Transform spawnPoint;
    public float timeToSpawnBoss = 300f;
    private bool _bossHasSpawned = false;
    [Header("UI Settings")]
    public GameObject bossWarningUI;
    void Start()
    {
      
        if (bossWarningUI != null)
        {
            bossWarningUI.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnTheBoss();
        }
        if (!_bossHasSpawned)
        {
            CheckAutoSpawn();
        }
    }
    void CheckAutoSpawn()
    {
        if (GameTime.Instance != null)
        {
            if (Time.timeSinceLevelLoad >= timeToSpawnBoss)
            {
                _bossHasSpawned = true;
                SpawnTheBoss();
            }
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
        if (spawnedBoss.CompareTag("Boss"))
        {
            StartCoroutine(ShowBossUIForSeconds(6f));
        }
        else
        {
            Debug.LogWarning("BossSpawner: Spawned item doesnt have boss tag");
        }
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SwitchToBossMusic();
        }
        else
        {
            Debug.LogWarning("BossSpawner: AudioManager not found");
        }
    }
    private IEnumerator ShowBossUIForSeconds(float duration)
    {
        if (bossWarningUI != null)
        {
            bossWarningUI.SetActive(true); 

            yield return new WaitForSeconds(duration); 

            bossWarningUI.SetActive(false); 
        }
        else
        {
            Debug.LogWarning("BossSpawner: BossUI isnt given");
        }
    }
}