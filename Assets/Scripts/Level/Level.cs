using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class Level : MonoBehaviour
    {
        public string CurrentScene;
        public string NextScene;
        public bool IsBossLevel;
        public int SpawnCount;

        private void Start()
        {
            Game.Bind(Game.ChangeLevel, ChangeLevel);
        }

        private void OnDestroy()
        {
            Game.Unbind(Game.ChangeLevel, ChangeLevel);
        }

        private void ChangeLevel(GameObject entity)
        {
            SpawnCount--;
            if (SpawnCount > 0)
                return;
            if (IsBossLevel)
                LoadEndScene();
            else
                LoadScene();
        }

        private void LoadScene()
        {
            Game.AddScene(NextScene);
            Game.UnLoadScene(CurrentScene);
        }

        private void LoadEndScene()
        {
            Game.LoadScene(NextScene);
        }
    }
}

