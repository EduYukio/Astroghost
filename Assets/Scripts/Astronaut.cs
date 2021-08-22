using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 speedVector;
    public float angularSpeed;
    public bool changeSprite = false;
    public SpriteRenderer spriteRenderer;
    public Sprite spriteToChange;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = speedVector * speed;
        GetComponent<Rigidbody2D>().angularVelocity = angularSpeed;
        if (changeSprite) Invoke(nameof(ChangeAstroSprite), 4f);
    }

    private void ChangeAstroSprite()
    {
        spriteRenderer.sprite = spriteToChange;
        transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Rigidbody2D>().velocity = Vector3.up * 1f;
    }
}
