using UnityEngine;

public class RangedEnemyBehavior : EnemyBehavior
{
    [Header("Custom Ranged Settings")]
    public float minDistance = 3f;        //the radius he needs to stay out of

    [Header("Custom Drop Settings")]
    public GameObject specialItemPrefab;  //HealthDrop
    [Range(0f, 1f)]
    public float specialDropChance = 0.2f; //this is equal to 20%

    // overrides the movement
    protected override void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < minDistance)
        {
           //makes sure to stay away from player
            Vector3 targetPosition = transform.position + (transform.position - playerTransform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            FlipSprite();
        }
        else if (distanceToPlayer > stopDistance)
        {
            //if he is to far away he moves to the player
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            FlipSprite();
        }
    }

    // Override DropLogic
    protected override void DropXP()
    {
        //Makes it so the xp drop stil works
        base.DropXP();

        //here the 20% drop is made
        if (specialItemPrefab != null && Random.value <= specialDropChance)
        {
            Vector2 randomOffset = Random.insideUnitCircle * dropSpreadRadius;
            Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
            Instantiate(specialItemPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("The health has been spawned");
        }
    }
}