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

    // public Vector2 initialPosition;

    void Start()
    {
        // initialPosition = transform.localPosition;
        // timeCounter = initi
    }

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        // timeCounter += Time.deltaTime * speed;
        timeCounter += Time.deltaTime * speed;
        float x = Mathf.Cos(timeCounter + angleFactor) * radius;
        float y = Mathf.Sin(timeCounter + angleFactor) * radius;
        transform.localPosition = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.TakeDamage(damage);
        }
    }
}
