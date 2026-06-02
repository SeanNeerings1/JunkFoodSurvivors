using UnityEngine;

public class PowerUp2 : PlayerShoot//made it a child
{
    protected override void Shoot()
    {
        if (bulletPrefab == null) return;

        if (shootSound != null && shootSound.clip != null)
        {
            shootSound.PlayOneShot(shootSound.clip);
        }

        Vector2 moveDirection = Vector2.right;

        if (_arduinoScript != null && _arduinoScript.enabled)
        {
            moveDirection = _arduinoScript.lastMoveDirection;
        }
        else if (_keyboardScript != null && _keyboardScript.enabled)
        {
            moveDirection = _keyboardScript.lastMoveDirection;
        }

        Vector2 forwardDirection = moveDirection.normalized;
        Vector2 backwardDirection = -forwardDirection;  //what this does is it says remember the forward direction do that but in the - and that = backward direction

        SpawnSingleBullet(forwardDirection);
        SpawnSingleBullet(backwardDirection);
    }
}