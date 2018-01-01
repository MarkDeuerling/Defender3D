using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class GameStart : MonoBehaviour 
    {
        public void OnStart()
        {
            SceneManager.LoadScene("Game");
        }   
        public void OnTutorial()
        {
            SceneManager.LoadScene("Tutorial");
        }        
    }
}
