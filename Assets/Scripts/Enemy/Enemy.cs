using Directives;
using Projectiles;
using Statics;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public Shoot Shooting;
        public EnemyMovement Movement;
        public int Health = 1;
        public float LifeTime = 10;

        private Rigidbody body;
        private Timer timer = new Timer();

        private void Start()
        {
            body = this.GetRigidBody();
            Destroy(gameObject, LifeTime);
        }

        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (!timer.IsTimeUp(Time.deltaTime, Shooting.FireRate))
                return;
            var bullet = Instantiate(Shooting.Bullet);
            bullet.SetPosition(this.GetPosition() + Shooting.Offset);
            timer.Reset();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var dt = Time.fixedDeltaTime;
            var velocity = 
                new Vector3(-Movement.MoveSpeed, Movement.SinusMove(dt));
            body.MovePosition(body.position + velocity * dt);
        }

        private void OnTriggerEnter(Collider entity)
        {
            HitOnBullet(entity);
            HitOnSpecial(entity);
        }

        private void HitOnBullet(Collider entity)
        {
            if (entity.HasNot(Tag.Bullet))
                return;
            Health--;
            Destroy(entity.gameObject);
            HealthCondition();
        }

        private void HitOnSpecial(Collider entity)
        {
            if (entity.HasNot(Tag.Special))
                return;
            Health = 0;
            Destroy(entity.gameObject);
            HealthCondition();
        }

        private void HealthCondition()
        {
            if (Health > 0)
                return;
            Game.Execute(Game.ScoreUpdate, gameObject);
            Destroy(gameObject);
        }
    }
}