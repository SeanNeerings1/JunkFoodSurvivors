using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerHealthBarTest : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth;

    public HealthBar healthBar;

    public AudioSource audioSource;
    public AudioClip damageSound;

    [Header("Flashing Settings")]
    public SpriteRenderer playerSprite; //player sprite here
    public float flashDuration = 0.15f;

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
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
        if (playerSprite != null && CurrentHealth > 0)
        {
            StartCoroutine(FlashRed());
        }

        if (CurrentHealth <= 0)
        {
            SceneManager.LoadScene("DeathScene");
        }
    }
    public void Heal(int amount)
    {
        CurrentHealth += amount;

        // makes sure it doesnt go higher
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        // updates visual healthbar
        if (healthBar != null)
        {
            healthBar.SetHealth(CurrentHealth);
        }

    }
    private IEnumerator FlashRed()
    {
        playerSprite.color = Color.red; //Changes player to red
        yield return new WaitForSeconds(flashDuration); // time for flash
        playerSprite.color = Color.white; // changes color back
    }
}