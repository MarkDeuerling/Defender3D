using Directives;
using Statics;
using UnityEngine;

namespace Enemy.Boss
{
    [System.Serializable]
    public class ArmShoot
    {
        public GameObject BulletPref;
        public int Count;
        public float SpawnRate;
        public Vector3 Offset;

        private const float Angle = 180;
        private Timer timer = new Timer();
        private int count;
        private bool canShoot;

        public void Initialize()
        {
            count = Count;
            CanShoot = true;
            canShoot = true;
        }

        public void Update(float dt)
        {
            if (!timer.IsTimeUp(dt, SpawnRate))
                return;
            timer.Reset();
            canShoot = true;
        }
        
        public bool CanShoot { get; private set; }

        public void Shoot(GameObject entity)
        {
            if (count <= 0)
                CanShoot = false;
            if (!canShoot)
                return;
            --count;
            Spawn(entity);
        }

        private void Spawn(GameObject entity)
        {
            if (!canShoot)
                return;
            canShoot = false;
            var bullet = Object.Instantiate(BulletPref);
            bullet.SetPosition(entity.GetPosition() + Offset);
            var randomAngle = Random.Range(0, 45);
            var rotation = Vector3.right * (randomAngle + Angle);
            var seed = Random.value;
            rotation.x *= seed >= 0.5f ? 1 : -1;
            bullet.transform.eulerAngles += rotation;
            bullet.GetBullet().Direction = bullet.transform.forward;
        }
    }
}