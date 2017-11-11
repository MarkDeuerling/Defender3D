using UnityEngine;

namespace Enemy
{
    [System.Serializable]
    public struct SinusMovement
    {
        public float MoveSpeed;
        public float Amplitude;
        private float elapsedTime;

        public float SinusMove(float dt)
        {
            elapsedTime += dt;
            var sinusMove = Mathf.Sin(MoveSpeed * elapsedTime);
            sinusMove *= Amplitude;
            return sinusMove;
        }
    }
}