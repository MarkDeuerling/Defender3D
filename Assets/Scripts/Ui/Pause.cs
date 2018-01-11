using GameState;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class Pause : MonoBehaviour
    {
        public void OnRestart()
        {
            Game.LoadScene("Game");   
            Game.AddScene("Environment");
            Game.AddScene("Camera");
        }
        
        public void OnResume()
        {
            Time.timeScale = 1f;
            Game.UnloadScene(Game.Pause);
            Game.CurrentState = new PlayerState();
        }

        public void OnClose()
        {
            SceneManager.LoadScene("Start");
            SceneManager.LoadScene("Camera", LoadSceneMode.Additive);
            SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
        }
    }
}