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
            if (!Game.CurrentState.Shoot)
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
            var velocity = Game.CurrentState.Move;
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
            Game.Execute(Game.Hit, gameObject);
        }

        private void HealthCondition()
        {
            if (Health <= 0)
                Game.LoadScene(Game.GameOver);
        }
    }
}