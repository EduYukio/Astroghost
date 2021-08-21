using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isGrounded = false;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    private SpriteRenderer spriteRenderer;
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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCamera = Camera.main;
    }

    void Update()
    {
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
            playerCamera.orthographicSize = Mathf.Lerp(playerCamera.orthographicSize, 5, zoomOutSpeed);
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
}
