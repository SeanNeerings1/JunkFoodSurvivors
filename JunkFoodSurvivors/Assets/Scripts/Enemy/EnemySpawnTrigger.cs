using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] private EnemySpawn _player;
    [SerializeField] private float _difficultyMultiplier = 1f;
    
    private float _spawnTimer = 0f;
    private float _baseSpawnInterval = 1.5f;
    private float _difficultyTimer = 0f;
    private float _difficultyInterval = 5f;   // How often difficulty increases (in seconds)

    private void Update()
    {
        // Increase both timers every frame
        _spawnTimer += Time.deltaTime;
        _difficultyTimer += Time.deltaTime;

        HandleSpawning();
        HandleDifficultyScaling();
    }

    private void HandleSpawning()
    {
        // Calculate current spawn interval based on difficulty
        float currentInterval = _baseSpawnInterval / _difficultyMultiplier;

        if (_spawnTimer < currentInterval)
            return;

        _spawnTimer = 0f;
        _player.SpawnEnemy();
    }

    private void HandleDifficultyScaling()
    {
        // Only increase difficulty every set interval
        if (_difficultyTimer < _difficultyInterval)
            return;

        // Reset timer after applying difficulty increase
        _difficultyTimer = 0f;

        // Gradually increase difficulty over time
        _difficultyMultiplier += 0.1f;

        // Prevent difficulty from scaling infinitely
        _difficultyMultiplier = Mathf.Clamp(_difficultyMultiplier, 1f, 10f);
    }
}