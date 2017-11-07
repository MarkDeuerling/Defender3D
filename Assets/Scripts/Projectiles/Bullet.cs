using Statics;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class Bullet : MonoBehaviour
    {
        public float MoveSpeed;
        public Vector3 Direction;
        public float DestroyTime = 5f;

        private Rigidbody body;

        private void Start()
        {
            body = this.GetRigidBody();
            Destroy(gameObject, DestroyTime);
        }

        private void FixedUpdate()
        {
            var velocity = Direction * MoveSpeed;
            body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
        }
    }
}