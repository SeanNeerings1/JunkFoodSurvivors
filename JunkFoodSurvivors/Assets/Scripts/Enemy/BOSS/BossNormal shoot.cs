using UnityEngine;
public class BossNormalShoot : BossShoot
{
    [Header("Normal Boss Shoot Settings")]
    public GameObject normalBulletPrefab;
    public float normalFireRate = 1f;
    public float normalBulletSpeed = 12f;  
    protected override void Start()
    {
        //starts the start of the base script
        base.Start();
        // new variables
        if (normalBulletPrefab != null)
        {
            bulletPrefab = normalBulletPrefab;
        }
        fireRate = normalFireRate;
        bulletSpeed = normalBulletSpeed;
    }
    //override shoot function 4 sides
    protected override void Shoot()
    {
        // plays shoot sound
        if (shootSound != null)
        {
            shootSound.Play();
        }
        // shoot in all four directions
        SpawnSingleBullet(Vector2.up);
        SpawnSingleBullet(Vector2.down);
        SpawnSingleBullet(Vector2.left);
        SpawnSingleBullet(Vector2.right);
        Debug.Log("Boss shout in al four directions");
    }
}