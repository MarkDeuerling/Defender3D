using GameState;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class Pause : MonoBehaviour
    {
        private void Start()
        {
            Camera.main.GetComponent<BackgroundMusic>().SetVolume(0.03f);
        }

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
            Camera.main.GetComponent<BackgroundMusic>().SetVolume(0.1f);
        }

        public void OnClose()
        {
            SceneManager.LoadScene("Start");
            SceneManager.LoadScene("Camera", LoadSceneMode.Additive);
            SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
        }
    }
}