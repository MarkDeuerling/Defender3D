using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Game game;
    private const string BulletTag = "Bullet";

    public void Init(Game game)
    {
        this.game = game;
    }

    void Start()
    {
        transform.position = new Vector3(5f, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D entity)
    {
        var isBullet = entity.CompareTag(BulletTag);
        if (isBullet)
            Destroy(gameObject);
    }
}
