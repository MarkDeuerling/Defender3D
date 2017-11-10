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
        public float UseCount;
        public Vector3 Center;
        public Vector3 HitBox;
        public Collider[] colliders;

        public void Shoot(Vector3 position)
        {
            Physics.OverlapBoxNonAlloc(
                Center, 
                HitBox, 
                colliders, 
                Quaternion.identity, 
                1 << LayerMask.NameToLayer(Tag.Hitable));
            colliders.ToList().ForEach(collider => FindTarget(collider, position));
        }

        private void FindTarget(Component collider, Vector3 position)
        {
            if (collider == null)
                return;
            var entity = Object.Instantiate(Bullet);
            var bullet = entity.GetComponent<Projectiles.Special>();
            bullet.SetPosition(position);
            bullet.Target = collider.gameObject;
        }
    }
}
