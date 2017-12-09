using Directives;
using Projectiles;
using Statics;
using UnityEngine;

namespace Enemy.Boss
{
	public class Boss : MonoBehaviour
	{
		public int Health;
		public GameObject HitPref;
		public GameObject DiePref;
		public SpreadShoot SpreadShoot;
		public MouthShoot MouthShoot;
		public ArmShoot ArmShoot; 

		private Timer timer = new Timer();
		private Timer mouthTimer = new Timer();
		private Timer armTimer = new Timer();
		private bool isBullets;
		private int count = 10;

		private void OnTriggerEnter(Collider other)
		{
			if (other.HasNot(Tag.Bullet))
				return;
			--Health;
			Spawn(HitPref, other.gameObject, 0.5f);
			HealthCondition(other.gameObject);
		}

		private void HealthCondition(GameObject entity)
		{
			if (Health >= 0)
				return;
			Spawn(DiePref, entity, 3f);
			Destroy(gameObject);
		}

		private void Spawn(GameObject prefab, GameObject hit, float destroyTime)
		{
			var spawned = Instantiate(prefab);
			spawned.SetPosition(hit.GetPosition());
			Destroy(spawned, destroyTime);
		}

		private void Update()
		{
			RandomShoot();
		}

		private void RandomShoot()
		{
			if (count <= 0)
				isBullets = false;
			if (isBullets && count > 0)
			{
				ShootBullets();
				count--;
				
				return;
			}
			count = 10;
			var seed = Random.Range(0, 3);
			switch (seed)
			{
				case 0:
					MouthLaser();
					break;
				case 1:
					EyeShoot();
					break;
				case 2:
					isBullets = true;
					break;
			}
		}

		private void ShootBullets()
		{
			if (!armTimer.IsTimeUp(Time.deltaTime, ArmShoot.SpawnRate)) return;
			ArmShoot.Shoot(gameObject);
			armTimer.Reset();
		}
		
		private void MouthLaser()
		{
			if (!mouthTimer.IsTimeUp(Time.deltaTime, MouthShoot.ShootRate)) return;
			MouthShoot.Shoot(gameObject, this);
			mouthTimer.Reset();
		}

		private void EyeShoot()
		{
			if (!timer.IsTimeUp(Time.deltaTime, SpreadShoot.ShootRate)) return;
			SpreadShoot.Shoot(gameObject);
			timer.Reset();
		}
	}
}
