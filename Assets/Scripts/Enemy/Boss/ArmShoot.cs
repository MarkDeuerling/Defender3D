using System;
using Statics;
using UnityEngine;

namespace Enemy.Boss
{
    [Serializable]
    public class ArmShoot
    {
        public GameObject BulletPref;
        public float SpawnRate;
        public Vector3 Offset;

        private const float Angle = 180;

        public void Shoot(GameObject entity)
        {
            var bullet = GameObject.Instantiate(BulletPref);
            bullet.SetPosition(entity.GetPosition() + Offset);
            var randomAngle = UnityEngine.Random.Range(0, 45);
            var rotation = Vector3.right * (randomAngle + Angle);
            var seed = UnityEngine.Random.value;
            rotation.x *= seed >= 0.5f ? 1 : -1;
            bullet.transform.eulerAngles += rotation;
            bullet.GetBullet().Direction = bullet.transform.forward;
        }
    }
}