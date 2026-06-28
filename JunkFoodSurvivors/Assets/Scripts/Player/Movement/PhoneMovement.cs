using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneMovement : MonoBehaviour
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
        FlipSprite();
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
    public void UI_StartMove(string richting)
    {
        switch (richting.ToLower())
        {
            case "up": moveDirection = new Vector2(0, 1); break;
            case "down":  moveDirection = new Vector2(0, -1); break;
            case "left":   moveDirection = new Vector2(-1, 0); break;
            case "right":  moveDirection = new Vector2(1, 0); break;
        }
        if (moveDirection != Vector2.zero)
        {
            lastMoveDirection = moveDirection;
        }
    }
    public void UI_StopMove()
    {
        moveDirection = Vector2.zero;
    }

    void FlipSprite()
    {
        if (moveDirection.x > 0) spriteRenderer.flipX = true;
        else if (moveDirection.x < 0) spriteRenderer.flipX = false;
    }
}