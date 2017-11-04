using UnityEngine;

namespace Statics
{
    public static class Extensions
    {
        public static Rigidbody GetRigidBody(this MonoBehaviour behaviour)
        {
            return behaviour.GetComponent<Rigidbody>();
        }
    
        public static Vector3 GetPosition(this MonoBehaviour behaviour)
        {
            return behaviour.transform.position;
        }

        public static void SetPosition(this GameObject entity, Vector3 position)
        {
            entity.transform.position = position;
        }
    
        public static bool HasNot(this Collider collider, string tag)
        {
            return !collider.gameObject.CompareTag(tag);
        }
    }
}