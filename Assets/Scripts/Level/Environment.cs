using UnityEngine;

namespace Level
{
    public class Environment : MonoBehaviour
    {
        public int MoveSpeed;

        private void Update()
        {
            transform.Translate(MoveSpeed * Time.deltaTime,0,0);
        }
    }
}
