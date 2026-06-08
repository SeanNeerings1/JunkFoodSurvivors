using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [Header("Movement & Range")]
    public float maxDistance = 5f;

    [Header("Explosion Settings")]
    public float radius = 3.0f;
    public int maxDamage = 100;
    public float fuseTimeAfterStop = 1.0f;

    [Header("Effects & Audio")]
    public ParticleSystem explosionEffect;
    public AudioClip explosionSound;
    [Range(0f, 1f)] public float volume = 1.0f;

    private Rigidbody2D _rb;
    private Vector2 _startPosition;
    private bool _hasStopped = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

    public void Setup(Vector2 direction, float speed)
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb != null)
        {
            _rb.linearVelocity = direction.normalized * speed;
        }
    }

    void Update()
    {
        if (_hasStopped) return;

        if (Vector2.Distance(_startPosition, transform.position) >= maxDistance)
        {
            StopAndPrepareExplosion();
        }
    }

    void StopAndPrepareExplosion()
    {
        _hasStopped = true;

        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero;
        }

        Invoke("Explode", fuseTimeAfterStop);
    }

    void Explode()
    {
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, volume);
        }

        if (explosionEffect != null)
        {
            ParticleSystem effectKopie = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effectKopie.Play();
            Destroy(effectKopie.gameObject, 2.0f);
        }

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D hit in hitObjects)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealthBarTest player = hit.GetComponent<PlayerHealthBarTest>();
                if (player != null)
                {
                    float distance = Vector2.Distance(transform.position, hit.transform.position);
                    float relativeDistance = Mathf.Clamp01((radius - distance) / radius);
                    int finalDamage = Mathf.RoundToInt(relativeDistance * maxDamage);

                    player.SendMessage("TakeDamage", finalDamage, SendMessageOptions.DontRequireReceiver);
                    Debug.Log($"Boss bullet did {finalDamage} damage to Player");
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