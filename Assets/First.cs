using UnityEngine;
using UnityEngine.SceneManagement;

public class First : MonoBehaviour 
{
    private void Start()
    {
        SceneManager.LoadScene("Start");
        SceneManager.LoadScene("Camera", LoadSceneMode.Additive);
        SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
    }
}
