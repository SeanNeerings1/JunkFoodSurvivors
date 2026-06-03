using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject EnemyPrefab;
    public Transform player;
    public float minRadius = 3f;
    public float maxRadius = 6f;
    void Start()
    {
        
    }

 
    // Update is called once per frame
    void Update()
    {
        
    }

   public void SpawnEnemy()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        float distance = Random.Range(minRadius, maxRadius);
        

        Vector2 spawnPosition = (Vector2)player.position + direction * distance;

       GameObject enemy = Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("spawnEnemy has been called");
    }



}
