using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public int healAmount = 20; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //searches for healthbarscript
            PlayerHealthBarTest playerHealth = collision.GetComponent<PlayerHealthBarTest>();

            if (playerHealth != null)
            {
                if (playerHealth.CurrentHealth < playerHealth.MaxHealth)
                {
                    playerHealth.Heal(healAmount);
                    Destroy(gameObject);
                }
            }
        }
    }
}