using UnityEngine;

namespace GameState
{
    public class PlayerState : IGameState
    {
        private const string H = "Horizontal";
        private const string V = "Vertical";
        private const string F = "Fire1";

        public Vector3 Move
        {
            get { return new Vector3(Input.GetAxis(H), Input.GetAxis(V)); }
        }
        
        public bool Shoot { get { return Input.GetButton(F); } }

        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.P))
                return;
            Game.AddScene(Game.Pause);
            Time.timeScale = 0;  
            Game.CurrentState = new PauseState();
        }
    }
}