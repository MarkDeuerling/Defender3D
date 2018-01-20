using System.Linq;
using Statics;
using UnityEngine;

namespace Player
{
    [System.Serializable]
    public class Special
    {
        public GameObject Bullet;
        public float FireRate;
        public int UseCount;
        public Vector3 Center;
        public Vector3 HitBox;
        public Collider[] Colliders;

        private Vector3 position;
        
        public bool HasTarget { get; private set; }

        public void Shoot(Vector3 position)
        {
            this.position = position;
            var target = Physics.OverlapBoxNonAlloc(
                Center, 
                HitBox, 
                Colliders, 
                Quaternion.identity, 
                1 << LayerMask.NameToLayer(Tag.Hitable));
            HasTarget = target > 0;
            Colliders
                .Where(e => e != null)
                .ToList()
                .ForEach(FindTarget);
        }

        private void FindTarget(Collider collider)
        {
            if (collider.Has("Boss"))
            {
                HasTarget = false;
                return;
            }
            var entity = Object.Instantiate(Bullet);
            var bullet = entity.GetSpecialBullet();
            bullet.SetPosition(position + new Vector3(0,0,Random.value*10));
            bullet.Target = collider.gameObject;
        }
    }
}
