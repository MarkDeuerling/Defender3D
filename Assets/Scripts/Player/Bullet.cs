using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed { get; set; }
    public Vector2 Direction { get; set; }
    public float LifeTime { get; set; }

    private float time;

    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > LifeTime)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        var velocity = Direction * Speed;
        body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
    }
}
