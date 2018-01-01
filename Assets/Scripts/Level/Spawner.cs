using Directives;
using Statics;
using UnityEngine;

namespace Level
{
    public class Spawner : MonoBehaviour
    {
        public GameObject Enemy;
        public int Count;
        public float SpawnRate;
        public float StartSpawn;

        private Timer timer;
        private Timer startTimer;
        private int currentCount;

        private void Start()
        {
            timer = new Timer();
            startTimer = new Timer();
            Game.Bind(Game.BossDied, OnBossDie);
        }

        private void OnBossDie(GameObject entity)
        {
            Destroy(gameObject);
        }
        
        private void Update()
        {
            if (!startTimer.IsTimeUp(Time.deltaTime, StartSpawn))
                return;
            if (currentCount == Count)
                Destroy(gameObject);
            if (!timer.IsTimeUp(Time.deltaTime, SpawnRate)) 
                return;
            Instantiate(Enemy).SetPosition(this.GetPosition());
            timer.Reset();
            currentCount++;
        }

        private void OnDestroy()
        {
            Game.Unbind(Game.BossDied, OnBossDie);
            Game.Execute(Game.ChangeLevel, gameObject);
        }
    }
}