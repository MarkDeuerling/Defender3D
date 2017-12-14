using Directives;
using Statics;
using UnityEngine;
using VolumetricLines;

namespace Enemy.Boss
{
    [System.Serializable]
    public class MouthShoot
    {
        public GameObject Laser;
        public float ShootRate;
        public Vector3 Offset;

        private VolumetricLineBehavior line;
        private Timer timer = new Timer();
        private bool canShoot;
        
        public bool IsLaserShooting { get; set; }

        public void Update(float dt)
        {
            if (timer.IsTimeUp(dt, ShootRate))
                canShoot = true;
        }

        public void Shoot(GameObject entity)
        {
            if (!canShoot)
                return;
            canShoot = false;
            timer.Reset();
            IsLaserShooting = true;
            var laser = Object.Instantiate(Laser);
            line = laser.GetComponent<VolumetricLineBehavior>();
            var offset = Offset - Vector3.right * line.EndPos.z;
            laser.SetPosition(entity.GetPosition() + offset);
        }
    }
}