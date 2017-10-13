using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float ShootRate;

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string Fire = "Fire1";
    private Rigidbody2D body;
    private Game game;
    private float time;

    public void Init(Game game)
    {
        this.game = game;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time < ShootRate)
            return;

        time = 0;

        var hasFired = Input.GetButton(Fire);
        if (hasFired)
            Shoot();
    }

    void Shoot()
    {
        var bullet = Instantiate<Bullet>(game.BulletPref);
        var position = transform.position;
        var offset = new Vector3(position.x + 1, position.y, position.z);
        bullet.transform.position = offset;

        bullet.Speed = 10;
        bullet.Direction = Vector2.right;
        bullet.LifeTime = 5f;
    }

    void FixedUpdate()
    {
        MovePosition();
    }

    void MovePosition()
    {
        var horizontal = Input.GetAxis(Horizontal);
        var vertical = Input.GetAxis(Vertical);

        var velocity = new Vector2(horizontal, vertical) * Speed;

        body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
    }
}
