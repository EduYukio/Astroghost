using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    public Transform frontTransform;
    private float distanceToCheckGround = 0.2f;
    public float moveSpeed = 1f;

    void Start()
    {

    }

    void Update()
    {
        if (!ThereIsGroundToWalk())
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
}