using UnityEngine;

namespace Enemy
{
    [System.Serializable]
    public struct LineMovement
    {
        public float MoveSpeed;
        public Vector3 Direction;

        public Vector3 Velocity { get { return MoveSpeed * Direction; } }
    }
}
