﻿using Statics;
using UnityEngine;

namespace Projectiles
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(BoxCollider))]
	public class DirectionBullet : MonoBehaviour 
	{
		public float MoveSpeed;
		public float DestroyTime = 5f;

		private Rigidbody body;
		private Vector3 direction;
		
		public GameObject Target { private get; set; }

		private void Start()
		{
			body = this.GetRigidBody();
			direction = this.GetDirectionTo(Target);
			Destroy(gameObject, DestroyTime);
		}

		private void FixedUpdate()
		{
			var velocity = direction * MoveSpeed;
			body.Move(velocity);
		}
	}
}
