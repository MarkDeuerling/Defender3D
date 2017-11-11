using Directives;
using Projectiles;
using Statics;
using UnityEngine;

namespace Enemy
{
	[System.Serializable]
	public class MoveShoot
	{
		public float MoveSpeed;
		public Vector3 InPosition;
		public Vector3 OutDirection;
		public float ThreshHold;
		public float StayTime;
		public Shoot Shooting;
		public GameObject TargetPlayer;
		
		public int State { get; set; }

		public Vector3 MoveIn(Vector3 position, float dt)
		{
			return Vector3.MoveTowards(position, InPosition, dt * MoveSpeed);
		}

		public Vector3 Velocity()
		{
			return MoveSpeed * OutDirection;
		}

		public bool IsInPosition(Vector3 position)
		{
			return Vector3.Distance(position, InPosition) <= ThreshHold;
		}

		public Quaternion FacePlayer(Transform transform)
		{
			if (TargetPlayer == null)
				return Quaternion.identity;
			var targetPosition = TargetPlayer.GetPosition() - transform.position;
			return Quaternion.LookRotation(targetPosition);
		}
		
	}
}
