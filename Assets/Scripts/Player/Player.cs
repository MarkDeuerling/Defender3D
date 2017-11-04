using Directives;
using Projectiles;
using Statics;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public Shoot Shooting;
        public int Health = 1;
        public float MoveSpeed = 10;
        
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string Fire = "Fire1";
        private Rigidbody body;
        private Timer timer = new Timer();


        private void Start()
        {
            body = this.GetRigidBody();
            Game.Execute(Game.PlayerHealthUpdate, gameObject);
        }

        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (!timer.IsTimeUp(Time.deltaTime, Shooting.FireRate))
                return;
            if (!Input.GetButton(Fire))
                return;
            SpawnBullet();
            timer.Reset();
        }

        private void SpawnBullet()
        {
            var bullet = Instantiate(Shooting.Bullet);
            bullet.SetPosition(body.position + Shooting.Offset);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var horizontal = Input.GetAxis(Horizontal);
            var vertical = Input.GetAxis(Vertical);
            var velocity = new Vector3(horizontal, vertical);
            velocity *= MoveSpeed;
            var position = body.position + velocity * Time.fixedDeltaTime;
            body.MovePosition(Clamp(position));
        }

        private static Vector3 Clamp(Vector3 position)
        {
            position = Camera.main.WorldToViewportPoint(position);
            position.x = Mathf.Clamp01(position.x);
            position.y = Mathf.Clamp01(position.y);
            return Camera.main.ViewportToWorldPoint(position);
        }

        private void OnTriggerEnter(Collider entity)
        {
            if (entity.HasNot(Tag.Enemy))
                return;
            Health--;
            Destroy(entity.gameObject);
            HealthCondition();
            Game.Execute(Game.PlayerHealthUpdate, gameObject);
        }

        private void HealthCondition()
        {
            if (Health <= 0)
                Game.OnGameOver();
        }
    }
}