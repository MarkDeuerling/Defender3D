using Statics;
using UnityEngine;

namespace GameCamera
{
    public class GameCamera : MonoBehaviour 
    {
        public float ShakeDuration;
        public float ShakeAmount = 0.7f;
        public float DecreaseFactor = 1.0f;
	
        private Vector3 cachePosition;
        private float shakeDuration;
        private float enemyShakeAmount;
        private float cacheShakeAmount;

        private void Start()
        {
            enemyShakeAmount = ShakeAmount * 0.5f;
            cacheShakeAmount = ShakeAmount;
            cachePosition = this.GetPosition();
            Game.Bind(Game.Hit, OnHit);
            Game.Bind(Game.DestroyEnemy, OnDestroyEnemy);
        }

        private void OnDestroy()
        {
            Game.Unbind(Game.Hit, OnHit);
        }

        private void OnHit(GameObject entity)
        {
            shakeDuration = ShakeDuration;
            ShakeAmount = cacheShakeAmount;
        }

        private void OnDestroyEnemy(GameObject entity)
        {
            shakeDuration = ShakeDuration;
            ShakeAmount = enemyShakeAmount;
        }

        private void Update()
        {
            if (shakeDuration > 0 && Time.timeScale >= 1)
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
