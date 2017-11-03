using Directives;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public int Health = 1;
        public GameObject Bullet;
        public float FireRate;
        public Vector3 Offset;
        public float MoveSpeed;
        public float Amplitude;
        public float LifeTime = 10;

        private Rigidbody body;
        private Timer timer = new Timer();
        private float elapsedTime;

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
            if (!timer.IsTimeUp(Time.deltaTime, FireRate))
                return;
            var bullet = Instantiate(Bullet);
            bullet.SetPosition(this.GetPosition() + Offset);
            timer.Reset();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            elapsedTime += Time.deltaTime;
            var sinusMove = Mathf.Sin(MoveSpeed * elapsedTime) * Amplitude;
            var velocity = new Vector3(-MoveSpeed, sinusMove, 0);
            body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter(Collider entity)
        {
            if (entity.HasNot(Tag.Bullet))
                return;
            Health--;
            Destroy(entity.gameObject);
            HealthCondition();
        }

        private void HealthCondition()
        {
            if (Health <= 0)
                Destroy(gameObject);
        }
    }
}