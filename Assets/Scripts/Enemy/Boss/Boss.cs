using Directives;
using Projectiles;
using UnityEngine;

namespace Enemy.Boss
{
	public class Boss : MonoBehaviour
	{
		public SpreadShoot SpreadShoot;
		public MouthShoot MouthShoot;
		public ArmShoot ArmShoot; 

		public float Rate;
		private Timer timer = new Timer();
		private Timer mouthTimer = new Timer();
		private Timer armTimer = new Timer();

		private void Start()
		{
			mouthTimer.HotStart();
		}

		private void Update()
		{
			ShootBullets();
//			EyeShoot();
//			MouthLaser();
		}

		private void ShootBullets()
		{
			if (!armTimer.IsTimeUp(Time.deltaTime, ArmShoot.SpawnRate)) return;
			ArmShoot.Shoot(gameObject);
			armTimer.Reset();
		}
		
		private void MouthLaser()
		{
			if (!mouthTimer.IsTimeUp(Time.deltaTime, Rate)) return;
			MouthShoot.Shoot(gameObject, this);
			mouthTimer.Reset();
		}

		private void EyeShoot()
		{
			if (!timer.IsTimeUp(Time.deltaTime, 1f)) return;
			SpreadShoot.Shoot(gameObject);
			timer.Reset();
		}
	}
}
