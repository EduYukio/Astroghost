using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    Player player;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    public float speed = 2f;

    bool isBeingHit = false;
    public float knockbackSpeed = 3f;
    public float beingHitTimer = 0.5f;

    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    void Update()
    {
        if (!isBeingHit)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
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

    public override void BeingHitFeedback()
    {
        Vector2 distance = player.transform.position - transform.position;
        Vector2 direction = -1 * distance.normalized;

        isBeingHit = true;
        rb.velocity = direction * knockbackSpeed;
        Invoke(nameof(StopBeingHit), beingHitTimer);
    }

    private void StopBeingHit()
    {
        isBeingHit = false;
    }
}
