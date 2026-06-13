using UnityEngine;

public class EnemyDiagonalShoot : BossShoot
{
    [Header("Diagonal Boss Shoot Settings")]
    public GameObject diagonalBulletPrefab; 
    public float diagonalFireRate = 1f;   
    public float diagonalBulletSpeed = 10f;  

    protected override void Start()
    {
        base.Start();

        if (diagonalBulletPrefab != null)
        {
            bulletPrefab = diagonalBulletPrefab;
        }

        fireRate = diagonalFireRate;
        bulletSpeed = diagonalBulletSpeed;
    }

    // override shoot and change direction
    protected override void Shoot()
    {
        if (shootSound != null)
        {
            shootSound.Play();
        }

       // makes it so the boss shoots diagnal and normalized makes sure all bullets are just as fast
        Vector2 upRight = new Vector2(1f, 1f).normalized;
        Vector2 upLeft = new Vector2(-1f, 1f).normalized;
        Vector2 downRight = new Vector2(1f, -1f).normalized;
        Vector2 downLeft = new Vector2(-1f, -1f).normalized;

        // Schiet in de 4 schuine hoeken
        SpawnSingleBullet(upRight);
        SpawnSingleBullet(upLeft);
        SpawnSingleBullet(downRight);
        SpawnSingleBullet(downLeft);

        Debug.Log("Boss shoots diagnal");
    }
}