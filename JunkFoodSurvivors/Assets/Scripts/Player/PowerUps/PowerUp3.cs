using UnityEngine;

public class PowerUp3 : PlayerShoot
{ //overides the shoot function of the basic script
    protected override void Shoot()
    {
        if (bulletPrefab == null) return;
        if (shootSound != null && shootSound.clip != null)
        {
            shootSound.PlayOneShot(shootSound.clip);
        }

        SpawnSingleBullet(Vector2.up);
        SpawnSingleBullet(Vector2.down);
        SpawnSingleBullet(Vector2.left);
        SpawnSingleBullet(Vector2.right);

        // diagonal directions and normalized else the diagonal ones move faster idk why

        SpawnSingleBullet(new Vector2(1, 1).normalized);
        SpawnSingleBullet(new Vector2(-1, 1).normalized);
        SpawnSingleBullet(new Vector2(1, -1).normalized);
        SpawnSingleBullet(new Vector2(-1, -1).normalized);
    }
}