using UnityEngine;

namespace Statics
{
    public static class Extensions
    {
        public static Rigidbody GetRigidBody(this MonoBehaviour behaviour)
        {
            return behaviour.GetComponent<Rigidbody>();
        }

        public static Projectiles.Special GetSpecialBullet(
            this GameObject entity)
        {
            return entity.GetComponent<Projectiles.Special>();
        }

        public static Player.Player GetPlayer(this GameObject entity)
        {
            return entity.GetComponent<Player.Player>();
        }
        
        public static Vector3 GetPosition(this MonoBehaviour behaviour)
        {
            return behaviour.transform.position;
        }

        public static Vector3 GetPosition(this GameObject entity)
        {
            return entity.transform.position;
        }

        public static void SetRotation(
            this GameObject entity, Quaternion rotation)
        {
            entity.transform.rotation = rotation;
        }

        public static void SetPosition(this GameObject entity, Vector3 position)
        {
            entity.transform.position = position;
        }
        
        public static void SetPosition(
            this MonoBehaviour behaviour, Vector3 position)
        {
            behaviour.transform.position = position;
        }
    
        public static bool HasNot(this Collider collider, string tag)
        {
            return !collider.gameObject.CompareTag(tag);
        }
        
        public static bool Has(this Collider collider, string tag)
        {
            return collider.gameObject.CompareTag(tag);
        }

        public static Vector3 GetDirectionTo(
            this MonoBehaviour behaviour, GameObject entity)
        {
            return (entity.GetPosition() - behaviour.GetPosition()).normalized;
        }

        public static Quaternion RotateTo(
            this MonoBehaviour behaviour, GameObject entity)
        {
            return Quaternion.LookRotation(behaviour.GetDirectionTo(entity));
        }

        public static void Move(this Rigidbody body, Vector3 velocity)
        {
            body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
        }
    }
}