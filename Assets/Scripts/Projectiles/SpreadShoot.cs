using Statics;
using UnityEngine;

namespace Projectiles
{
    [System.Serializable]
    public class SpreadShoot
    {
        public GameObject BulletPref;
        public float Spread;
        public int Count;
        public Vector3 Offset;
        public float ShootRate;

        private const int Angle = 180;
        
        public void Shoot(GameObject entity)
        {
            for (var i = 0; i < Count; ++i)
                SpreadBullet(entity, i);
        }

        private void SpreadBullet(GameObject entity, int i)
        {
            var bullet = Object.Instantiate(BulletPref);
            bullet.SetPosition(entity.GetPosition() + Offset);
            var mul = i % 2 == 0 ? i : -1 * i; 
            var factor = Angle + Spread * mul;
            var totalSpread = Vector3.right * factor;
            bullet.transform.eulerAngles += totalSpread;
            bullet.GetBullet().Direction = bullet.transform.forward;
        }
    }
}