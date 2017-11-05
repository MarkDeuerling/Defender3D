using UnityEngine;

namespace GameState
{
    public class PauseState : IGameState
    {
        public Vector3 Move { get { return Vector3.zero; } }
        public bool Shoot { get { return false; }}

        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.P))
                return;
            Game.UnLoadScene(Game.Pause);
            Time.timeScale = 1;
            Game.CurrentState = new PlayerState();
        }
    }
}