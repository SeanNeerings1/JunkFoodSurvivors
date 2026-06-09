using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int health = 100;
    public int damage = 10;
    public float moveSpeed = 3.5f;

    [Header("Movement Settings")]
    public float stopDistance = 0.5f;

    [Header("Drop Settings")]
    public GameObject xpPrefab;
    public int xpDropCount = 3; 
    public float dropSpreadRadius = 0.5f;

    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("EnemyBehavior: Kan geen object vinden met de tag 'Player'!");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        if (playerTransform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (playerTransform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            int bulletDamage = 25;
            TakeDamage(bulletDamage);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Enemy neemt damage! Health over: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

   protected virtual void Die()
    {
        Debug.Log("Enemy is verslagen!");
        if (gameObject.CompareTag("Enemy"))
        {//calling for kill meter to increase the amount
            if (KillMeter.Instance != null)
            {
                KillMeter.Instance.AddKill();
            }
            else
            {
                Debug.LogWarning("KillMeter is not found!");
            }
            DropXP();
        }
        Destroy(gameObject);
    }
    void DropXP()
    {
        if (xpPrefab == null)
        {
            Debug.LogWarning("Geen xpPrefab toegewezen op " + gameObject.name);
            return;
        }

        for (int i = 0; i < xpDropCount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * dropSpreadRadius;
            Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
        //spawns the xp orbs in random positions
            Instantiate(xpPrefab, spawnPosition, Quaternion.identity);
        }
    }
}