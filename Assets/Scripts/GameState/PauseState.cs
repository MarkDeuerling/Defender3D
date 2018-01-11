using UnityEngine;

namespace GameState
{
    public class PauseState : IGameState
    {
        public Vector3 Move { get { return Vector3.zero; } }
        public bool Shoot { get { return false; } }
        public bool Special { get { return false; } }

        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.P))
                return;
            Camera.main.GetComponent<BackgroundMusic>().SetVolume(0.1f);
            Game.UnloadScene(Game.Pause);
            Time.timeScale = 1;
            Game.CurrentState = new PlayerState();
        }
    }
}