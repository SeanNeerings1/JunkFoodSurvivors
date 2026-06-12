using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class BoomKoolLogic : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject bombPrefab;
    public bool isPrefab = true;
    public float spawnCooldown = 5.0f;

    [Header("explosion settings")]
    public float Radius = 3.0f;
    public int maxDamage = 100;
    public float fuseTime = 5.0f;
    private float nextSpawnTime = 0f;

    [Header("effects")]
    public ParticleSystem explosionEffect;

    [Header("Audio Settings")]
    public AudioClip explosionSound;
    [Range(0f, 1f)] public float volume = 1.0f;

    private bool isExploded = false;


    void Update()
    {
        if (isPrefab && Input.GetKeyDown(KeyCode.B)&& Time.time >= nextSpawnTime)
        {
            SpawnBombOnPlayer();
            nextSpawnTime = Time.time + spawnCooldown;
        }
    }

    void SpawnBombOnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && bombPrefab != null)
        {
            GameObject newBomb = Instantiate(bombPrefab, player.transform.position, Quaternion.identity);

            BoomKoolLogic newBombScript = newBomb.GetComponent<BoomKoolLogic>();
            if (newBombScript != null)
            {
                newBombScript.isPrefab = false;
                newBombScript.StartTimer();
            }

            Debug.Log("Bomb dropped you have 5 sec");
        }
        else if (player == null)
        {
            Debug.LogError("can not spawn bomb make sure that player has a player tag");
        }
    }

    public void StartTimer()
    {
        Invoke("Explode", fuseTime);
    }

    void Explode()
    {
        isExploded = true;
        if (explosionSound != null)
        {
            AudioSource cameraAudio = Camera.main.GetComponent<AudioSource>();
            if (cameraAudio != null)
            {
                cameraAudio.PlayOneShot(explosionSound, volume);
            }
            else
            {
                AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, volume);
            }
        }

        if (explosionEffect != null)
        {
            ParticleSystem effectKopie = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effectKopie.Play();

            Destroy(effectKopie.gameObject, 2.0f);
        }
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, Radius);

        foreach (Collider2D hit in hitObjects)
        {
            if (hit.CompareTag("Enemy") || hit.CompareTag("Boss"))
            {
                EnemyBehavior enemy = hit.GetComponent<EnemyBehavior>();
                if (enemy != null)
                {
                    //Clamp01 makes sure it always stays between 0 and 1 so it can never be negative

                    float distance = Vector2.Distance(transform.position, hit.transform.position);
                    float relativeDistance = Mathf.Clamp01((Radius - distance) / Radius);
                    int finalDamage = Mathf.RoundToInt(relativeDistance * maxDamage);

                    enemy.TakeDamage(finalDamage);
                    Debug.Log($"did {finalDamage} damage againt enemy");
                }
            }
        }
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        //makes it so you can see a red circle in the editor for radius of damage
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}