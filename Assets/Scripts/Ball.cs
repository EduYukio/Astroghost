using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 3f;
    public float radius = 2f;
    private float timeCounter = 0f;
    public float angleFactor = 0.2f;
    public int damage = 1;
    Player player;
    public GameObject effect;
    public float startParticleCooldown = 0.2f;
    public float particleTimer = 0f;
    public Sprite normalSprite;
    public Sprite attackingSprite;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        player = FindObjectOfType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Rotate();
        if (particleTimer <= 0)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            particleTimer = startParticleCooldown;
        }
        else
        {
            particleTimer -= Time.deltaTime;
        }
    }

    private void Rotate()
    {
        timeCounter += Time.deltaTime * speed;
        float x = Mathf.Cos(timeCounter + angleFactor) * radius;
        float y = Mathf.Sin(timeCounter + angleFactor) * radius;
        transform.localPosition = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (!enemy.canBeHit) return;
            enemy.TakeDamage(damage);
            enemy.canBeHit = false;
            spriteRenderer.sprite = attackingSprite;
            Invoke(nameof(TurnBackToNormalSprite), 0.1f);
        }
    }

    public void TurnBackToNormalSprite()
    {
        spriteRenderer.sprite = normalSprite;
    }
}
