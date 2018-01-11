using System.Collections;
using Transition;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class GameStart : MonoBehaviour
    {
        private CamMove camMove;
        public GameObject PlayerTransition;
        public float WaitTimePlayer;
        public float WaitTimeScene;
        
        private void Start()
        {
            camMove = Camera.main.GetComponent<CamMove>();
            camMove.Positioning();
            camMove.enabled = false;
            PlayerTransition.SetActive(false);
        }

        public void OnStart()
        {
            StartCoroutine(Transition());
        }

        private IEnumerator Transition()
        {
            PlayerTransition.SetActive(true);
            yield return new WaitForSeconds(WaitTimePlayer);
            camMove.enabled = true;
            camMove.Startup();
            yield return new WaitForSeconds(WaitTimeScene);
            SceneManager.LoadScene("Game", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("Start");
        }
        
        public void OnTutorial()
        {
            SceneManager.LoadScene("Tutorial");
        }        
    }
}
