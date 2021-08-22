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
    public GameObject effect;
    public bool canBeHit = true;

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
        if (health <= 0)
        {
            Manager.audio.Play("Hit3");
            DieMotion();
            Instantiate(effect, transform.position, Quaternion.identity);
            player.needToZoom = true;
            player.playerCamera.transform.position = transform.position + new Vector3(0, 0, -10);
        }
        else
        {
            BeingHitFeedback();
            Manager.audio.Play("HitSimple1");
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

        Animator animator = GetComponent<Animator>();
        animator.enabled = false;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = deadSprite;
        player.spriteRenderer.sprite = player.wowSprite;
        Invoke(nameof(TurnBackToNormalSprite), 0.15f);

        isDead = true;

        rb.bodyType = RigidbodyType2D.Dynamic;
        // rb.velocity = new Vector3(direction.x * 3f, 6f, 0);
        rb.velocity = new Vector3(direction.x * 7f, 12f, 0);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.angularVelocity = direction.x * -60f;
        rb.gravityScale = 2f;

        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.75f);
        // Manager.audio.PlayDelayed("EnemyDying", 0.75f);
    }

    public void TurnBackToNormalSprite()
    {
        player.spriteRenderer.sprite = player.normalSprite;
    }
}
