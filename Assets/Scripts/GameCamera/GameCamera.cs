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
            originalPos = transform.localPosition;
            Game.Bind(Game.Hit, OnHit);
        }

        private void OnHit(GameObject entity)
        {
            shakeDuration = ShakeDuration;
        }

        private void Update()
        {
            if (shakeDuration > 0)
            {
                transform.localPosition = originalPos + Random.insideUnitSphere * ShakeAmount;
                shakeDuration -= Time.deltaTime * DecreaseFactor;
            }
            else
            {
                transform.localPosition = originalPos;
            }
        }

    }
}
