using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;

    [Header("Audio")]
    public AudioSource shootSound;

    private float _nextFireTime = 0f;

    protected virtual void Start()
    {
        if (shootSound == null)
        {
            shootSound = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + fireRate;
        }
    }

    protected virtual void Shoot()
    {
        if (bulletPrefab == null) return;

        if (shootSound != null)
        {
            shootSound.Play();
        }

        SpawnSingleBullet(Vector2.up);
        SpawnSingleBullet(Vector2.down);
        SpawnSingleBullet(Vector2.left);
        SpawnSingleBullet(Vector2.right);
    }

    protected void SpawnSingleBullet(Vector2 direction)
    {
        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        BossBullet bulletScript = bullet.GetComponent<BossBullet>();
        if (bulletScript != null)
        {
            bulletScript.Setup(direction, bulletSpeed);
        }
        else
        {
            Debug.LogWarning("De kogel prefab mist het 'BossBullet' script!");
        }
    }
}