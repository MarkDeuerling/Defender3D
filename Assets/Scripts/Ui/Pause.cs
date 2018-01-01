using GameState;
using UnityEngine;

namespace Ui
{
    public class Pause : MonoBehaviour
    {
        public void OnRestart()
        {
            Game.LoadScene("Game");    
        }
        
        public void OnResume()
        {
            Time.timeScale = 1f;
            Game.UnloadScene(Game.Pause);
            Game.CurrentState = new PlayerState();
        }

        public void OnClose()
        {
            Application.Quit();
        }
    }
}