using UnityEngine;

public class EnemyBullet : BossBullet
{
    [Header("Lifetime Settings")]
    public float lifeTime = 7.0f; // live time normally 7 sec can be changed per object its on
    [Header("Enemy damage so you can change")]
    public int EnemyShootDamage = 15;
    // use start to destroy after curtain time
    protected virtual void Start()
    {
        maxDamage = EnemyShootDamage;
        Destroy(gameObject, lifeTime);
    }
    // makes it so it doesnt stop at 5 meters
    protected virtual void Update()
    {
      
    }

    //if he hits something he stops
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthBarTest player = collision.GetComponent<PlayerHealthBarTest>();
            if (player != null)
            {
                player.SendMessage("TakeDamage", maxDamage, SendMessageOptions.DontRequireReceiver);
            }

            Destroy(gameObject); // directly bye bye after impact
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // if hit wall bullet bye bye
        }
    }
}