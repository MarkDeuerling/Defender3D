using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gui
{
    public class Restart : MonoBehaviour
    {
        private const string Game = "Game";
        public void OnRestart()
        {
            SceneManager.LoadScene(Game);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(Game);
        }
    }
}