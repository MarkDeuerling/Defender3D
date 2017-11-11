using Statics;
using UnityEngine;

namespace Projectiles
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(BoxCollider))]
	public class Special : MonoBehaviour {

		public float MoveSpeed;
		public float DestroyTime = 5f;

		private Rigidbody body;

		public GameObject Target { private get; set; }

		private void Start()
		{
			body = this.GetRigidBody();
			Destroy(gameObject, DestroyTime);
		}

		private void FixedUpdate()
		{
			LookAtTarget();
			Move();
		}

		private void LookAtTarget()
		{
			if (!Target)
				return;
			body.rotation = this.RotateTo(Target);
		}

		private void Move()
		{
			var velocity = transform.forward * MoveSpeed;
			body.Move(velocity);
		}
	}
}
