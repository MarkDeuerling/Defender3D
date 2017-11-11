using System.Linq;
using Directives;
using Projectiles;
using Statics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public Shoot Shooting;
        public EnemyMovement Movement;
        public LineMovement Line;
        public MoveShoot MoveAndShoot;
        public int Health = 1;
        public float LifeTime = 10;

        private Rigidbody body;
        private Timer timer = new Timer();
        private Timer st = new Timer();

        private void Start()
        {
            body = this.GetRigidBody();
            Destroy(gameObject, LifeTime);
            
            foreach (var rootGameObject in SceneManager
                .GetSceneByName("Player")
                .GetRootGameObjects())
            {
                if (!rootGameObject.CompareTag("Player")) 
                    continue;
                MoveAndShoot.TargetPlayer = rootGameObject;
                break;
            }
        }

        private void Update()
        {
//            Shoot();
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
            Move3();
        }

        private void Move3()
        {
            var dt = Time.fixedDeltaTime;
            switch (MoveAndShoot.State)
            {
                case 0:
                    if (MoveAndShoot.IsInPosition(this.GetPosition()))
                        Shoot2(dt);
                    else
                    {
                        var velocity = MoveAndShoot.MoveIn(this.GetPosition(), dt);
                        body.MovePosition(velocity);    
                    }
                    break;
                case 1:
                    transform.eulerAngles = new Vector3(0,90,0);
                    body.MovePosition(body.position + MoveAndShoot.Velocity() * dt);
                    break;
            }
        }

        private void Shoot2(float dt)
        {
            if (timer.IsTimeUp(dt, MoveAndShoot.StayTime))
            {
                MoveAndShoot.State = 1;
            }
            else
            {
                if (!st.IsTimeUp(dt, MoveAndShoot.Shooting.FireRate))
                    return;
                st.Reset();
                transform.rotation = MoveAndShoot.FacePlayer(transform);
                transform.eulerAngles = -transform.eulerAngles;
                var bullet = Instantiate(MoveAndShoot.Shooting.Bullet);
                bullet.GetComponent<DirectionBullet>().Target =
                    MoveAndShoot.TargetPlayer;
                bullet.SetPosition(this.GetPosition());
                bullet.transform.rotation = transform.rotation;   
            }
        }

        private void Move2()
        {
            var velocity = Line.Direction * Line.MoveSpeed;
            body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
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