using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    EnemySpawn enemy;
    private int enemysToSpawn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemysToSpawn = 4; //think of formula lter
        for (int i = 0; i < enemysToSpawn; i++)
        {
            enemy.SpawnEnemy();
        }
    }
}
   