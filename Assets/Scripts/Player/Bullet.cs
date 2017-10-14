using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public Vector2 Direction;
    public float LifeTime;

    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, LifeTime);
    }

    void FixedUpdate()
    {
        var velocity = Direction * Speed;
        body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
    }
}
