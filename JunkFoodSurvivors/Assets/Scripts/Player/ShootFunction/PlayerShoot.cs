using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 12f;
    public float fireRate = 0.2f;      

    [Header("Audio")]
    public AudioSource shootSound;

    private float _nextFireTime = 0f;
    private PlayerMovement _keyboardScript;
    private PlayerMovementArduino _arduinoScript;

    void Start()
    {
        _keyboardScript = GetComponent<PlayerMovement>();
        _arduinoScript = GetComponent<PlayerMovementArduino>();
        //if you forget to place shootsound
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
        Vector2 finalShootDirection = Vector2.right; 

        if (_arduinoScript != null && _arduinoScript.enabled)
        {
            finalShootDirection = _arduinoScript.lastMoveDirection;
        }
        else if (_keyboardScript != null && _keyboardScript.enabled)
        {
            finalShootDirection = _keyboardScript.lastMoveDirection;
        }
        SpawnSingleBullet(finalShootDirection.normalized);
    }
    protected void SpawnSingleBullet(Vector2 direction)
    {
        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
}