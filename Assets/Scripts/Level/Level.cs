using UnityEngine;

namespace Level
{
    public class Level : MonoBehaviour
    {
        public string CurrentScene;
        public string NextScene;
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
            if (--SpawnCount > 0)
                return;
            LoadScene();
        }

        private void LoadScene()
        {
            Game.AddScene(NextScene);
            Game.UnloadScene(CurrentScene);
        }
    }
}

