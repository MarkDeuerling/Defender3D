using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private const string Player = "Player";
    private const string Level1 = "Level_1";
    private const string Gui = "GUI";
    
    private void Start()
    {
        LoadScene(Player);
        LoadScene(Level1);
        LoadScene(Gui);
    }

    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }
}
