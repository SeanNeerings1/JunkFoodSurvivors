using System.Collections;
using UnityEngine;

public class BossDash : BossShoot
{
    [Header("Dash Settings")]
    public float dashSpeed = 25f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 1f;

    [Header("Dash Damage Settings")]
    public int dashDamage = 30;          
    public string playerTag = "Player";
    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private EnemyBehavior _enemyBehavior;
    private bool _isDashing = false;
    private float _cooldownTimer = 0f;

    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
        _enemyBehavior = GetComponent<EnemyBehavior>();
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            _playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (_isDashing) return;
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }
        else if (_playerTransform != null)
        {
            Shoot();
        }
    }
    protected override void Shoot()
    {
        if (shootSound != null)
        {
            shootSound.Play();
        }
        StartCoroutine(PerformDash());
    }
    private IEnumerator PerformDash()
    {
        _isDashing = true;
        _cooldownTimer = dashCooldown;

        if (_enemyBehavior != null) _enemyBehavior.enabled = false;

        Vector2 dashDirection = (_playerTransform.position - transform.position).normalized;

        RigidbodyType2D originalType = _rb.bodyType;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        float originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0f;

        _rb.linearVelocity = dashDirection * dashSpeed;
        Debug.Log("Boss is dashing");

        yield return new WaitForSeconds(dashDuration);

        _rb.linearVelocity = Vector2.zero;
        _rb.bodyType = originalType;
        _rb.gravityScale = originalGravity;
        _isDashing = false;
        if (_enemyBehavior != null) _enemyBehavior.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Only does damage when he is dashing
        if (_isDashing && collision.CompareTag(playerTag))
        {
            PlayerHealthBarTest player = collision.GetComponent<PlayerHealthBarTest>();
            if (player != null)
            {
                //sends damage to player
                player.SendMessage("TakeDamage", dashDamage, SendMessageOptions.DontRequireReceiver);
                Debug.Log("bpss did damage during his dash");
            }
        }
    }
    //does damage even without trigger so this is a failsafe
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDashing && collision.gameObject.CompareTag(playerTag))
        {
            PlayerHealthBarTest player = collision.gameObject.GetComponent<PlayerHealthBarTest>();
            if (player != null)
            {
                player.SendMessage("TakeDamage", dashDamage, SendMessageOptions.DontRequireReceiver);
                Debug.Log("the boss even does damage even without the trigger");
            }
        }
    }
}