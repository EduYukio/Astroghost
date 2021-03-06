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
    public Animator animator;

    // public float distanceToPursuePlayer = 2f;

    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        health = maxHealth;
    }

    void Update()
    {
        if (isDead) return;
        if (!isBeingHit)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 distance = player.transform.position - transform.position;
        Vector2 direction = distance.normalized;

        // if (Mathf.Abs(distance.magnitude) > distanceToPursuePlayer) return;
        if (!spriteRenderer.isVisible) return;
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
        animator.SetBool("isBeingHit", true);

        rb.velocity = direction * knockbackSpeed;
        Invoke(nameof(StopBeingHit), beingHitTimer);
    }

    private void StopBeingHit()
    {
        isBeingHit = false;
        animator.SetBool("isBeingHit", false);
        canBeHit = true;
    }
}
