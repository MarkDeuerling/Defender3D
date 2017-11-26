using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gui
{
    public class Gameover : MonoBehaviour
    {
        private const string Game = "Game";
        public void OnRestart()
        {
            SceneManager.LoadScene(Game);
        }

        public void OnClose()
        {
            Application.Quit();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(Game);
        }
    }
}