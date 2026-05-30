using UnityEngine;

public class PowerUp1 : PlayerShoot
{ //overides the shoot function of the basic script
   protected override void Shoot()
    {
        if (bulletPrefab == null) return;

        SpawnSingleBullet(Vector2.up);
        SpawnSingleBullet(Vector2.down);
        SpawnSingleBullet(Vector2.left);
        SpawnSingleBullet(Vector2.right);
    }
}
