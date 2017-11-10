using Statics;
using UnityEngine;

namespace Projectiles
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(BoxCollider))]
	public class Special : MonoBehaviour {

		public float MoveSpeed;
		public Vector3 Direction;
		public float DestroyTime = 5f;

		private Rigidbody body;

		public GameObject Target { get; set; }

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
			var targetPosition = Target.GetPosition() - this.GetPosition();
			var direction = Quaternion.LookRotation(targetPosition);
			transform.eulerAngles = direction.eulerAngles + Direction;
		}

		private void Move()
		{
			var velocity = transform.up * MoveSpeed;
			body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
		}
	}
}
