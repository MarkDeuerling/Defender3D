﻿using System;
using Directives;
using Projectiles;
using Statics;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class Enemy : MonoBehaviour
    {
        public Shoot Shooting;
        public SinusMovement Sinus;
        public LineMovement Line;
        public MoveShoot MoveAndShoot;
        public int Health = 1;
        public float LifeTime = 10;
        public Pattern Patter;
        public GameObject HealthPowerUp;
        public GameObject SpecialPowerUp;

        private delegate void Movement();

        private Movement movement;
        private Rigidbody body;
        private Timer timer = new Timer();
        private Timer shootTimer = new Timer();

        private void Start()
        {
            body = this.GetRigidBody();
            Destroy(gameObject, LifeTime);
            ChoosePattern();   
        }

        private void ChoosePattern()
        {
            switch (Patter)
            {
                case Pattern.Sinus:
                    movement = SinusMove;
                    break;
                case Pattern.Line:
                    movement = LineMove;
                    break;
                case Pattern.MoveAndShoot:
                    movement = MoveWithShoot;
                    FindPlayer();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void FindPlayer()
        {
            MoveAndShoot.Target = Game.Player;
        }

        private void Update()
        {
            if (Patter == Pattern.Sinus || Patter == Pattern.Line)
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
            movement();
        }

        private void MoveWithShoot()
        {
            var dt = Time.fixedDeltaTime;
            switch (MoveAndShoot.State)
            {
                case 0:
                    if (MoveAndShoot.IsInPosition(this.GetPosition()))
                        ShootTarget(dt);
                    else
                    {
                        var velocity = MoveAndShoot.MoveIn(this.GetPosition(), dt);
                        body.MovePosition(velocity);    
                    }
                    break;
                case 1:
                    transform.eulerAngles = new Vector3(0,90,0);
                    body.Move(MoveAndShoot.Velocity);
                    break;
            }
        }

        private void ShootTarget(float dt)
        {
            if (timer.IsTimeUp(dt, MoveAndShoot.StayTime))
            {
                MoveAndShoot.State = 1;
            }
            else
            {
                if (!shootTimer.IsTimeUp(dt, MoveAndShoot.Shooting.FireRate))
                    return;
                shootTimer.Reset();
                transform.rotation = this.RotateTo(MoveAndShoot.Target);
                transform.eulerAngles = -transform.eulerAngles;
                
                var bullet = Instantiate(MoveAndShoot.Shooting.Bullet);
                bullet.GetComponent<DirectionBullet>().Target =
                    MoveAndShoot.Target;
                bullet.SetPosition(this.GetPosition());
                bullet.SetRotation(body.rotation);   
            }
        }

        private void LineMove()
        {
            body.Move(Line.Velocity);
        }

        private void SinusMove()
        {
            var velocity = new Vector3(
                -Sinus.MoveSpeed, Sinus.SinusMove(Time.fixedDeltaTime));
            body.Move(velocity);
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
            Instantiate(HealthPowerUp).SetPosition(this.GetPosition());
//            Instantiate(SpecialPowerUp).SetPosition(this.GetPosition());
            Destroy(gameObject);
        }
    }
}