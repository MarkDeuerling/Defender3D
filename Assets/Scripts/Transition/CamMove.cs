using UnityEngine;

namespace Transition
{
    public class CamMove : MonoBehaviour
    {
        public Transform StartTransform;
        public Transform PlayTransform;
        public float TransitionSpeed;
        public float RotationFactor;
        public float TransitionTime;

        private float rot;

        public void Positioning()
        {
            GetComponent<GameCamera.GameCamera>().enabled = false;
            transform.position = StartTransform.position;
            transform.rotation = StartTransform.rotation;
        }

        public void Startup()
        {
            Destroy(this, TransitionTime);
        }

        private void OnDestroy()
        {
            transform.position = PlayTransform.position;
            transform.rotation = PlayTransform.rotation;
            GetComponent<GameCamera.GameCamera>().enabled = true;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                PlayTransform.position, 
                TransitionSpeed * Time.deltaTime);
            
            rot += Time.deltaTime / RotationFactor;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                PlayTransform.rotation,
                rot);
        }
    }
}
