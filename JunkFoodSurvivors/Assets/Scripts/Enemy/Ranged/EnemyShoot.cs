using UnityEngine;

public class EnemySimpleShoot : MonoBehaviour
{
    [Header("Enemy Shoot Settings")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float bulletSpeed = 12f;
    public AudioSource shootSound;
    private float nextFireTime;
    void Update()
    {
       //firetime
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; //reset Timer
        }
    }
    // Shoot
    protected virtual void Shoot()
    {
        //plays Sound
        if (shootSound != null)
        {
            shootSound.Play();
        }
        //searches player with player tag
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null && bulletPrefab != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Spawns bullet at enemy position
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // gives bullet speed with rigid body
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
            }
        }
    }
}