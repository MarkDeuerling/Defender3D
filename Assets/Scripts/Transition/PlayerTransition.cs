using UnityEngine;

namespace Transition
{
    public class PlayerTransition : MonoBehaviour
    {
        public Transform PlayerTransform;
        public float Speed;
        public float TransitionTime;

        private void Start()
        {
            Destroy(gameObject, TransitionTime);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                PlayerTransform.position,
                Time.deltaTime * Speed);
        }
    }
}