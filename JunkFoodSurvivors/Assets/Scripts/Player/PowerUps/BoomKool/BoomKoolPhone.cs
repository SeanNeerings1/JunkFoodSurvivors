using UnityEngine;

public class BoomKoolPhone : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float radius = 3.0f;
    public int damage = 100;
    public float fuseTime = 3.0f;
    [Header("Effects & Audio")]
    public ParticleSystem explosionEffect;
    public AudioClip explosionSound;
    [Range(0f, 1f)] public float volume = 1.0f;
    void Start()
    {
        Invoke("Explode", fuseTime);
    }
    void Explode()
    {
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position, volume);
        }
        if (explosionEffect != null)
        {
            ParticleSystem effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, 2.0f);
        }
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hitObjects)
        {
            if (hit.CompareTag("Enemy") || hit.CompareTag("Boss"))
            {
                EnemyBehavior enemy = hit.GetComponent<EnemyBehavior>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    Debug.Log($"Bomb hit enemy");
                }
            }
        }
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}