using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool isGrounded = false;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public SpriteRenderer spriteRenderer;
    public float jumpForce = 5f;
    public float fallMultiplier = 3.5f;
    public float lowJumpMultiplier = 15f;

    public Camera playerCamera;
    public float zoomInSpeed = 0.2f;
    public float zoomOutSpeed = 0.2f;
    public bool needToZoom = false;

    public float zoomInSize = 3.75f;
    public float normalZoomSize = 5f;
    public float pauseTime = 0.1f;
    private bool alreadyInCoroutine = false;

    public float maxHealth = 3f;
    public float health;
    public bool isInvulnerable = false;
    public float invulnerabilityTime = 2f;

    public float newAlpha = 1f;

    public bool isDead = false;

    public Sprite normalSprite;
    public Sprite beingHitSprite;
    public Sprite wowSprite;

    public float maxFallSpeed = -10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCamera = Camera.main;
        health = maxHealth;
        Manager.PlayBGMIfNotStartedYet();
    }

    void Update()
    {
        if (isDead) return;


        if (needToZoom)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
            ProcessWalkRequest();
            ProcessJumpRequest();
        }

        if (isInvulnerable)
        {
            newAlpha -= newAlpha * 0.025f;
            if (newAlpha <= 0.4f) newAlpha = 1;
            spriteRenderer.color = new Color(126f / 255, 1f, 247f / 255, newAlpha);
        }
        else
        {
            newAlpha = 1;
            spriteRenderer.color = new Color(126f / 255, 1f, 247f / 255, 1f);
        }
    }

    private void ZoomIn()
    {
        Time.timeScale = 0.025f;
        playerCamera.orthographicSize = Mathf.Lerp(playerCamera.orthographicSize, zoomInSize, zoomInSpeed);
        if (Mathf.Approximately(playerCamera.orthographicSize, zoomInSize))
        {
            if (!alreadyInCoroutine)
            {
                StartCoroutine(nameof(TimePauseCoroutine));
            }
        }
    }

    private void ZoomOut()
    {
        playerCamera.transform.position = transform.position + new Vector3(0, 0, -10);
        bool notNormalSizeYet = !Mathf.Approximately(playerCamera.orthographicSize, normalZoomSize);
        if (notNormalSizeYet)
        {
            playerCamera.orthographicSize = Mathf.Lerp(playerCamera.orthographicSize, normalZoomSize, zoomOutSpeed);
            Time.timeScale = 1f;
        }
    }

    private void ProcessWalkRequest()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        UpdateLookingDirection(xInput);
        BetterJumping();
    }

    private void UpdateLookingDirection(float xInput)
    {
        if (xInput < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (xInput > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void ProcessJumpRequest()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    private void BetterJumping()
    {
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
            return;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    IEnumerator TimePauseCoroutine()
    {
        alreadyInCoroutine = true;
        yield return new WaitForSecondsRealtime(pauseTime);
        needToZoom = false;
        alreadyInCoroutine = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (isInvulnerable) return;
            health -= 1;
            spriteRenderer.sprite = beingHitSprite;
            Manager.audio.Play("PlayerTakeHit");
            Invoke(nameof(ChangeToNormalSprite), 1f);
            if (health <= 0)
            {
                Die();
                return;
            }
            isInvulnerable = true;
            //talvez animação de tomar hit
            Invoke(nameof(TurnOffInvulnerability), invulnerabilityTime);

        }
    }

    private void TurnOffInvulnerability()
    {
        isInvulnerable = false;
    }

    private void Die()
    {
        // Animator animator = GetComponent<Animator>();
        // animator.enabled = false;
        isDead = true;

        // spriteRenderer.sprite = deadSprite;
        rb.velocity = new Vector3(Vector2.left.x * 7f, 12f, 0);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.angularVelocity = Vector2.left.x * -60f;
        rb.gravityScale = 2f;
        GetComponent<Collider2D>().enabled = false;
        playerCamera.transform.parent = null;
        // Destroy(gameObject, 0.75f);
        // Manager.audio.PlayDelayed("EnemyDying", 0.75f);
        Invoke(nameof(ReloadScene), 0.75f);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ChangeToNormalSprite()
    {
        spriteRenderer.sprite = normalSprite;
    }
}
