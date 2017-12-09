using Directives;
using UnityEngine;

namespace Enemy.Boss
{
	public class Boss : MonoBehaviour
	{
		public Projectiles.SpreadShoot SpreadShoot;
		public MouthShoot MouthShoot;

		public float Rate;
		private Timer timer = new Timer();
		private Timer mouthTimer = new Timer();

		private void Start()
		{
			mouthTimer.HotStart();
		}

		private void Update()
		{
//			ArmShoot();
			MouthLaser();
		}

		private void MouthLaser()
		{
			if (!mouthTimer.IsTimeUp(Time.deltaTime, Rate)) return;
			MouthShoot.Shoot(gameObject, this);
			mouthTimer.Reset();
		}

		private void ArmShoot()
		{
			if (!timer.IsTimeUp(Time.deltaTime, 1f)) return;
			SpreadShoot.Shoot(gameObject);
			timer.Reset();
		}
	}
}
