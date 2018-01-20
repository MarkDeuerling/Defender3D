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

        private void Start()
        {
            StartCoroutine(Transition());
        }

        private IEnumerator Transition()
        {
            yield return new WaitForSeconds(CleanUpTime);
            GetPpCam().UseBlur = true;
            GetPpCam().BlurConfig.Blur = 0;
            while (GetPpCam().BlurConfig.Blur < MaxBlur)
            {
                GetPpCam().BlurConfig.Blur += BlurFactor * Time.deltaTime;
                yield return new WaitForSeconds(0.02f);
            }
            Instantiate(Boss, transform.position, transform.rotation);
            yield return new WaitForSeconds(.2f);
            GetPpCam().UseBlur = false;
        }

        private static PostProcessingCamera GetPpCam()
        {
            return Camera.main.GetComponent<PostProcessingCamera>();
        }
    }
}
