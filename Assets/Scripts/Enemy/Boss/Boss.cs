using Directives;
using UnityEngine;

namespace Enemy.Boss
{
	public class Boss : MonoBehaviour
	{
		public Projectiles.SpreadShoot SpreadShoot;

		private Timer timer = new Timer();

		private void Update()
		{
			if (!timer.IsTimeUp(Time.deltaTime, 1f)) return;
			SpreadShoot.Shoot(gameObject);
			timer.Reset();
		}
	}
}
