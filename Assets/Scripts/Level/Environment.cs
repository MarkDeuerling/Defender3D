using UnityEngine;

namespace Level
{
    public class Environment : MonoBehaviour
    {
        public int MoveSpeed;

        private void Start()
        {
            Game.Bind(Game.BossSpawn, OnBossSpawn);
        }

        private void OnDestroy()
        {
            Game.Unbind(Game.BossSpawn, OnBossSpawn);
        }

        private void OnBossSpawn(GameObject entity)
        {
            Destroy(this);
        }

        private void Update()
        {
            transform.Translate(MoveSpeed * Time.deltaTime,0,0);
        }
    }
}
