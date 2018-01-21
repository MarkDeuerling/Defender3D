using System.Collections;
using GameCamera;
using UnityEngine;

namespace Transition
{
    public class BossTransition : MonoBehaviour
    {
        public float MaxBlur;
        public float BlurFactor;
        public float CleanUpTime;
        public GameObject Boss;

        private PostProcessingCamera ppC;

        private void Start()
        {
            ppC = Camera.main.GetComponent<PostProcessingCamera>();
            StartCoroutine(Transition());
        }

        private IEnumerator Transition()
        {
            yield return new WaitForSeconds(CleanUpTime);
            ppC.UseBlur = true;
            ppC.BlurConfig.Blur = 0;
            
            while (ppC.BlurConfig.Blur < MaxBlur)
            {
                ppC.BlurConfig.Blur += BlurFactor * Time.deltaTime;
                yield return new WaitForSeconds(0.02f);
            }
            Instantiate(Boss, transform.position, transform.rotation);
            yield return new WaitForSeconds(.2f);
            ppC.UseBlur = false;
        }
    }
}
