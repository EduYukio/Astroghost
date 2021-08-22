using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    public Transform frontTransform;
    public Transform wallTransform;
    public float distanceToCheckGround = 0.2f;
    public float distanceToCheckWall = 0.2f;
    public float moveSpeed = 1f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        if (!ThereIsGroundToWalk() || ThereIsWallAhead())
        {
            InvertDirection();
        }
        else
        {
            Walk();
        }
    }

    private void Walk()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private bool ThereIsGroundToWalk()
    {
        int layersToCollide = LayerMask.GetMask("Ground");
        RaycastHit2D ray = Physics2D.Raycast(frontTransform.position, Vector2.down, distanceToCheckGround, layersToCollide);

        if (ray.collider != null && ray.collider.CompareTag("Ground"))
        {
            return true;
        }

        return false;
    }

    private bool ThereIsWallAhead()
    {
        int layersToCollide = LayerMask.GetMask("Ground");
        RaycastHit2D ray = Physics2D.Raycast(wallTransform.position, Vector2.left, distanceToCheckWall, layersToCollide);

        if (ray.collider != null && ray.collider.CompareTag("Ground"))
        {
            return true;
        }

        return false;
    }

    public void InvertDirection()
    {
        if (transform.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (transform.eulerAngles.y == 180)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    public override void BeingHitFeedback()
    {
        animator.SetBool("isBeingHit", true);
        Invoke(nameof(StopBeingHit), 0.15f);
    }

    private void StopBeingHit()
    {
        animator.SetBool("isBeingHit", false);
    }
}
