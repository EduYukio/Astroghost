using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 2f;
    public float health;
    Player player;
    public Sprite deadSprite;
    public bool isDead = false;

    void Start()
    {
        health = maxHealth;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        BeingHitFeedback();
        if (health <= 0)
        {
            DieMotion();
        }
    }

    public virtual void BeingHitFeedback()
    {

    }

    public void DieMotion()
    {
        if (player == null) player = GameObject.FindObjectOfType<Player>();
        Vector2 direction = -(player.transform.position - transform.position).normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = deadSprite;
        isDead = true;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = new Vector3(direction.x * 3f, 6f, 0);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.angularVelocity = direction.x * -40f;
        rb.gravityScale = 2f;

        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.75f);
        // Manager.audio.PlayDelayed("EnemyDying", 0.75f);
    }
}
