using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthBarTest : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth;

    public HealthBar healthBar;

    private bool raaktVijandAan = false;
    private float damageCooldown = 1.0f;
    private float lastDamageTime;

    void Start()
    {
        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
    }

    void Update()
    {
        if (raaktVijandAan && Time.time >= lastDamageTime + damageCooldown)
        {
            TakeDamage(10);
            lastDamageTime = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            raaktVijandAan = true;
            TakeDamage(10);        
            lastDamageTime = Time.time;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            raaktVijandAan = false;
        }
    }

    void TakeDamage(int damage)
    {
        CurrentHealth -= damage;


        if (CurrentHealth < 0) CurrentHealth = 0;

        healthBar.SetHealth(CurrentHealth);
    }
}