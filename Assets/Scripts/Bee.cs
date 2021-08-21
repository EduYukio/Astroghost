using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    Player player;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    public float speed = 2f;

    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    void Update()
    {
        SetSpeed();
    }

    private void SetSpeed()
    {
        Vector2 distance = player.transform.position - transform.position;
        Vector2 direction = distance.normalized;

        rb.velocity = direction * speed;
        UpdateLookingPosition(direction);
    }

    private void UpdateLookingPosition(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
