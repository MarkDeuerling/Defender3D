using UnityEngine;

public static class Extensions
{ 
    public static Rigidbody2D GetRigidbody(this MonoBehaviour physics)
    {
        return physics.GetComponent<Rigidbody2D>();
    }
}
