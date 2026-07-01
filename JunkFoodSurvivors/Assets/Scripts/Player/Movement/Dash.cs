using System.Collections;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private AbilityCooldownUI _cooldownUI;

    [Header("Dash settings")]
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float _dashingPower = 24f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;

    private Vector2 _currentInput;
    private float _lastDirection = 1f;

    private bool _canDash = true;
    public bool isDashing { get; private set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (tr != null)
            tr.emitting = false;
    }

    private void Update()
    {
      
        _currentInput = Vector2.zero;

        _currentInput.x = (Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0);
        _currentInput.y = (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);

      
        if (_currentInput.x != 0)
            _lastDirection = Mathf.Sign(_currentInput.x);

   
        if (Input.GetKeyDown(KeyCode.Q) && _canDash)
            StartCoroutine(Dash());
    }

    // Handles dash behaviour over time (movement, duration, cooldown)
    private IEnumerator Dash()
    {
        _canDash = false;
        isDashing = true;

        // Temporarily disable gravity so dash is not affected by falling/jumping
        float originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0f;

        // Direction of dash based on input, or fallback to last facing direction
        Vector2 dashDirection = _currentInput.normalized;

        if (dashDirection == Vector2.zero)
            dashDirection = Vector2.right * _lastDirection;

     
        _rb.linearVelocity = dashDirection * _dashingPower;

        if (tr != null)
            tr.emitting = true;

      
        yield return new WaitForSeconds(_dashingTime);

       
        _rb.linearVelocity = Vector2.zero;

        if (tr != null)
            tr.emitting = false;

      
        _rb.gravityScale = originalGravity;
        isDashing = false;

        _cooldownUI.StartCooldown(2);
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
}