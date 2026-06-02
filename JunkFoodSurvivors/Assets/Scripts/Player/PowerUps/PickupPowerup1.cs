using UnityEngine;

public class PickUpPowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShoot oldShoot = other.GetComponent<PlayerShoot>();

            if (oldShoot != null && !(oldShoot is PowerUp1))
            {
                GameObject prefab = oldShoot.bulletPrefab;
                Transform point = oldShoot.firePoint;
                float speed = oldShoot.bulletSpeed;
                float rate = oldShoot.fireRate;
                Destroy(oldShoot);

                PowerUp1 newShoot = other.gameObject.AddComponent<PowerUp1>();
                newShoot.bulletPrefab = prefab;
                newShoot.firePoint = point;
                newShoot.bulletSpeed = speed;
                newShoot.fireRate = rate;

                Debug.Log("Power-up activated");
            }

            Destroy(gameObject);
        }
    }
}