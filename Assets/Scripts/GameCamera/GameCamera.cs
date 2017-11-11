using Statics;
using UnityEngine;

namespace GameCamera
{
    public class GameCamera : MonoBehaviour {
        public float ShakeDuration;
        public float ShakeAmount = 0.7f;
        public float DecreaseFactor = 1.0f;
	
        private Vector3 cachePosition;
        private float shakeDuration;

        private void Start()
        {
            cachePosition = this.GetPosition();
            Game.Bind(Game.Hit, OnHit);
        }

        private void OnDestroy()
        {
            Game.Unbind(Game.Hit, OnHit);
        }

        private void OnHit(GameObject entity)
        {
            shakeDuration = ShakeDuration;
        }

        private void Update()
        {
            if (shakeDuration > 0)
                Shake();
            else
                this.SetPosition(cachePosition);
        }

        private void Shake()
        {
            transform.position = cachePosition + Random.insideUnitSphere * ShakeAmount;
            shakeDuration -= Time.deltaTime * DecreaseFactor;
        }

    }
}
