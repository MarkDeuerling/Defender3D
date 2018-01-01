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
        private PostProcessingCamera ppCam;

        private void Start()
        {
            enemyShakeAmount = ShakeAmount * 0.5f;
            cacheShakeAmount = ShakeAmount;
            cachePosition = this.GetPosition();
            Game.Bind(Game.Hit, OnHit);
            Game.Bind(Game.DestroyEnemy, OnDestroyEnemy);
            ppCam = GetComponent<PostProcessingCamera>();
        }

        private void GlitchCam(int mode)
        {
            switch (mode)
            {
                case 0:
                    ppCam.UseGlitch = true;
                    break;
                case 1:
                    ppCam.GlitchConfig.Distortion = Random.Range(-0.5f, 0.5f);
                    break;
                case 2:
                    ppCam.UseGlitch = false;
                    break;
            }
        }
        
        private void OnDestroy()
        {
            Game.Unbind(Game.Hit, OnHit);
            Game.Unbind(Game.DestroyEnemy, OnDestroyEnemy);
        }

        private void OnHit(GameObject entity)
        {
            shakeDuration = ShakeDuration;
            ShakeAmount = cacheShakeAmount;
            if (entity.Has(Tag.Player))
                GlitchCam(0);
        }

        private void OnDestroyEnemy(GameObject entity)
        {
            shakeDuration = ShakeDuration;
            ShakeAmount = enemyShakeAmount;
        }

        private void Update()
        {
            if (shakeDuration > 0 && Time.timeScale >= 1)
            {
                Shake();
                GlitchCam(1);
            }
            else
            {
                this.SetPosition(cachePosition);
            }
            if (shakeDuration <= 0)
                GlitchCam(2);
        }

        private void Shake()
        {
            transform.position = cachePosition + Random.insideUnitSphere * ShakeAmount;
            shakeDuration -= Time.deltaTime * DecreaseFactor;
        }

    }
}
