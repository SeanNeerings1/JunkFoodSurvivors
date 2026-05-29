using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 12f;
    public float fireRate = 0.2f;      

    private float _nextFireTime = 0f;
    private PlayerMovement _keyboardScript;
    private PlayerMovementArduino _arduinoScript;

    void Start()
    {
        _keyboardScript = GetComponent<PlayerMovement>();
        _arduinoScript = GetComponent<PlayerMovementArduino>();
    }

    void Update()
    {
        if (Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;

        Vector2 finalShootDirection = Vector2.right; 

       
        if (_arduinoScript != null && _arduinoScript.enabled)
        {
            finalShootDirection = _arduinoScript.lastMoveDirection;
        }
        else if (_keyboardScript != null && _keyboardScript.enabled)
        {
            finalShootDirection = _keyboardScript.lastMoveDirection;
        }
        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = finalShootDirection * bulletSpeed;
        }
    }
}