using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float LifeTime = 4f;

    private Game game;
    private const string BulletTag = "Bullet";
    private Rigidbody2D body;

    public void Init(Game game)
    {
        this.game = game;
    }

    void Start()
    {
        body = this.GetRigidbody();
        Destroy(gameObject, LifeTime);
    }
    
    void Update()
    {
        Move();
    }

    void Move()
    {
        var velocity = Vector2.left * MoveSpeed;
        body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D entity)
    {
        var isBullet = entity.CompareTag(BulletTag);
        if (isBullet)
            DestroyOnHit(entity.gameObject);
    }

    void DestroyOnHit(GameObject entity)
    {
        Destroy(gameObject);
        Destroy(entity);
    }
}
