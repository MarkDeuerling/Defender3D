using Directives;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public GameObject Bullet;
        public float FireRate = 0.5f;
        public int Health = 1;
        public float MoveSpeed = 10;
        public Vector3 Offset;
        
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string Fire = "Fire1";
        private Rigidbody body;
        private Timer timer = new Timer();


        private void Start()
        {
            body = this.GetRigidBody();
            Game.Execute(Game.OnPlayer, gameObject);
        }

        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (!timer.IsTimeUp(Time.deltaTime, FireRate))
                return;
            if (!Input.GetButton(Fire))
                return;
            SpawnBullet();
            timer.Reset();
        }

        private void SpawnBullet()
        {
            var bullet = Instantiate(Bullet);
            bullet.transform.position = body.position + Offset;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var horizontal = Input.GetAxis(Horizontal);
            var vertical = Input.GetAxis(Vertical);
            var velocity = new Vector3(horizontal, vertical, 0);
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
            Game.Execute(Game.OnPlayer, gameObject);
        }

        private void HealthCondition()
        {
            if (Health <= 0)
                Game.OnGameOver();
        }
    }
}