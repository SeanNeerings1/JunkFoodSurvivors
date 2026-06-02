using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    [HideInInspector] public Vector2 moveDirection = Vector2.zero;
    [HideInInspector] public Vector2 lastMoveDirection = Vector2.right;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Keyboard.current != null)
        {
            float x = 0f;
            float y = 0f;

            bool hasInput = false;

            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) { y = 1f; hasInput = true; }
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) { y = -1f; hasInput = true; }
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) { x = -1f; hasInput = true; }
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed){ x = 1f; hasInput = true;}

            moveDirection = new Vector2(x, y);

            if (moveDirection.magnitude > 1)
            {
                moveDirection.Normalize();
            }
            if (hasInput && moveDirection != Vector2.zero)
            {
                lastMoveDirection = moveDirection;
            }
        }
        FlipSprite();
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    void FlipSprite()
    {
        if (moveDirection.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}