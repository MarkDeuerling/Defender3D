using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void OnRestart()
    {
        SceneManager.LoadScene("Game");
    }
}