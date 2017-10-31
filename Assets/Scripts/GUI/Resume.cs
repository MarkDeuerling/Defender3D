using UnityEngine;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour
{
    public void OnResume()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Pause");
    }
}