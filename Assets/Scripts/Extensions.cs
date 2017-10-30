using UnityEngine;

public static class Extensions
{
    public static Rigidbody GetRigidBody(this MonoBehaviour behaviour)
    {
        return behaviour.GetComponent<Rigidbody>();
    }
    
    public static Vector3 GetPosition(this GameObject entity)
    {
        return entity.transform.position;
    }
    
    public static Vector3 GetPosition(this MonoBehaviour behaviour)
    {
        return behaviour.transform.position;
    }

    public static bool Has(this Collider collider, string tag)
    {
        return collider.gameObject.CompareTag(tag);
    }
    
    public static bool HasNot(this Collider collider, string tag)
    {
        return !collider.gameObject.CompareTag(tag);
    }
}