using Statics;
using UnityEngine;

namespace GameCamera
{
    public class GameCamera : MonoBehaviour {
        public float ShakeDuration;
        public float ShakeAmount = 0.7f;
        public float DecreaseFactor = 1.0f;
	
        private Vector3 originalPos;
        private float shakeDuration;

        private void Start()
        {
            originalPos = this.GetPosition();
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
                transform.position = originalPos;
        }

        private void Shake()
        {
            transform.position = originalPos + Random.insideUnitSphere * ShakeAmount;
            shakeDuration -= Time.deltaTime * DecreaseFactor;
        }

    }
}
