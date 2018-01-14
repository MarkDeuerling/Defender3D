using System.ComponentModel;
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
		public Movement Movement;

		private int maxHealth;
		private bool spawnOnHalf = true;
		private Animator animator;

		private void Start()
		{
			animator = GetComponent<Animator>();
			maxHealth = Health;
			ArmShoot.Initialize();
			SpreadShoot.Initialize();
			Game.Bind(Game.BossLaser, OnBossLaserStop);
			Game.Execute(Game.BossSpawn, gameObject);
			Movement.Init();
		}

		private void OnDestroy()
		{
			Game.Unbind(Game.BossLaser, OnBossLaserStop);
		}

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
			if (spawnOnHalf && Health <= maxHealth * 0.5f)
			{
				spawnOnHalf = false;
				Game.AddScene("BossWave");
			}
			if (Health >= 0)
				return;
			Spawn(DiePref, entity, 3f);
			Destroy(gameObject);
			Game.Execute(Game.BossDied, gameObject);
		}

		private static void Spawn(GameObject prefab, GameObject hit, float destroyTime)
		{
			var spawned = Instantiate(prefab);
			spawned.SetPosition(hit.GetPosition());
			Destroy(spawned, destroyTime);
		}

		private void OnBossLaserStop(GameObject entity)
		{
			MouthShoot.IsLaserShooting = false;
		}

		private void Update()
		{
			if (MouthShoot.IsLaserShooting)
				return;
			var dt = Time.deltaTime;
			MouthShoot.Update(dt);
			ArmShoot.Update(dt);
			SpreadShoot.Update(dt);
			RandomShoot();
			Movement.Move(dt, this);
		}

		private void RandomShoot()
		{
			
			var state = ChangeState();
			switch (state)
			{
				case 0:
					MouthLaser();
					break;
				case 1:
					EyeShoot();
					break;
				case 2:
					ShootBullets();
					break;
				default:
					throw new InvalidEnumArgumentException();
			}
		}

		private int ChangeState()
		{
			if (ArmShoot.CanShoot)
				return 2;
			ArmShoot.Initialize();
			return Random.Range(0, 3);
		}
		
		private void ShootBullets()
		{
			ArmShoot.Shoot(gameObject);
		}
		
		private void MouthLaser()
		{
			MouthShoot.Shoot(gameObject);
			animator.SetBool("Mouth", true);
		}

		private void EyeShoot()
		{
			SpreadShoot.Shoot(gameObject);
		}
	}
}
