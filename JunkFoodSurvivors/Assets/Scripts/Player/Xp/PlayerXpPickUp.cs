using UnityEngine;

public class XpPickup : MonoBehaviour
{
    [SerializeField] private int xpAmount = 25;
    [SerializeField] private AudioClip _pickUpSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerXp playerXp = other.GetComponent<PlayerXp>();

        if (playerXp != null)
        {
            playerXp.AddExperience(xpAmount);
            if(_pickUpSound != null)
            {
                AudioSource.PlayClipAtPoint(_pickUpSound, transform.position);
            }
            Destroy(gameObject);
        }
    }
}