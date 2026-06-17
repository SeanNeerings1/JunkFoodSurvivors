using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawn : MonoBehaviour
{
   

    [SerializeField] private int _enemy2SpawnRate; //Every how many Enemies spawn, that it instead spawns an enemy 2
    public GameObject EnemyPrefab;
    public GameObject EnemyPrefab2;
    public Transform player;
    public float minRadius = 3f;
    public float maxRadius = 6f;
    private int _enemiesSpawned = 0;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        float distance = Random.Range(minRadius, maxRadius);

        Vector2 spawnPosition = (Vector2)player.position + direction * distance;

        _enemiesSpawned++;

        GameObject prefabToSpawn;

        if (_enemiesSpawned % _enemy2SpawnRate == 0)
        {
            prefabToSpawn = EnemyPrefab2;
        }
        else
        {
            prefabToSpawn = EnemyPrefab;
        }

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        Debug.Log("SpawnEnemy has been called");
    }



}
