using UnityEngine;

public class XpPickup : MonoBehaviour
{
    [SerializeField] private int xpAmount = 25;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerXp playerXp = other.GetComponent<PlayerXp>();

        if (playerXp != null)
        {
            playerXp.AddExperience(xpAmount);
            Destroy(gameObject);
        }
    }
}