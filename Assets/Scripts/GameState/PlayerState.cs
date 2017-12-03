using UnityEngine;

namespace GameState
{
    public class PlayerState : IGameState
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string Fire = "Fire1";
        private const string Fire2 = "Jump";

        public Vector3 Move
        {
            get 
            { 
                return new Vector3(
                    Input.GetAxis(Horizontal), Input.GetAxis(Vertical)); 
            }
        }
        
        public bool Shoot { get { return Input.GetButton(Fire); } }
        public bool Special { get { return Input.GetButton(Fire2); } }

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