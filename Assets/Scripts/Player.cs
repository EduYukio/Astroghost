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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ProcessWalkRequest();
        ProcessJumpRequest();
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
}
