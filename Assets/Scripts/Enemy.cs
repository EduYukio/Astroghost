using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 2f;
    public float health;
    void Start()
    {
        health = maxHealth;
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
            Destroy(gameObject);
        }
    }

    public virtual void BeingHitFeedback()
    {

    }
}
