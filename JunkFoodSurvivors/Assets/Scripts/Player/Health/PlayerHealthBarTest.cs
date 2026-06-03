using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthBarTest : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth;

    public HealthBar healthBar;

    private bool _enemyContact = false;
    private float _damageCooldown = 1.0f; 
    private float _lastDamageTime;


    void Start()
    {
        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
    }

    void Update()
    {
        if (_enemyContact && Time.time >= _lastDamageTime + _damageCooldown)
        {
            TakeDamage(10);
            _lastDamageTime = Time.time; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyPrefab"))
        {
            _enemyContact = true; 
            TakeDamage(10);        
            _lastDamageTime = Time.time;
        }
    }

    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyPrefab"))
        {
            _enemyContact = false;
        }
    }

    void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        healthBar.SetHealth(CurrentHealth);
    }
}