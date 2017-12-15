using Statics;
using UnityEngine;

namespace GameCamera
{
    [System.Serializable]
    public class CameraShake
    {
        public float ShakeDuration;
        public float ShakeAmount = 0.7f;
        public float DecreaseFactor = 1.0f;
        
        private Vector3 cachePosition;
        private float shakeDuration;
        private float enemyShakeAmount;
        private float cacheShakeAmount;
        private GameObject entity;

        public void Initialize(GameObject entity)
        {
            enemyShakeAmount = ShakeAmount * 0.5f;
            cacheShakeAmount = ShakeAmount;
            cachePosition = entity.GetPosition();
            this.entity = entity;
        }
        
        private void Update()
        {
            if (shakeDuration > 0 && Time.timeScale >= 1)
                Shake();
            else
                entity.SetPosition(cachePosition);
        }
        
        private void Shake()
        {
            entity.transform.position = 
                cachePosition + Random.insideUnitSphere * ShakeAmount;
            shakeDuration -= Time.deltaTime * DecreaseFactor;
        }
    }
}