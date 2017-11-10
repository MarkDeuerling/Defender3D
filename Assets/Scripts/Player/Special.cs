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
        
        public bool HasTarget { get; private set; }

        public void Shoot(Vector3 position)
        {
            var target = Physics.OverlapBoxNonAlloc(
                Center, 
                HitBox, 
                colliders, 
                Quaternion.identity, 
                1 << LayerMask.NameToLayer(Tag.Hitable));
            HasTarget = target > 0;
            colliders.ToList().ForEach(collider => FindTarget(collider, position));
        }

        private void FindTarget(Component collider, Vector3 position)
        {
            if (collider == null)
                return;
            var entity = Object.Instantiate(Bullet);
            var bullet = entity.GetComponent<Projectiles.Special>();
            bullet.SetPosition(position + new Vector3(0,0,Random.value*10));
            bullet.Target = collider.gameObject;
        }
    }
}
