using UnityEngine;

namespace Projectiles
{
    [System.Serializable]
    public struct Shoot
    {
        public GameObject Bullet;
        public float FireRate;
        public Vector3 Offset;
    }
}