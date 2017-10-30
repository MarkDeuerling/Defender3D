using UnityEngine;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    private const string Player = "Player";
    private const string Level1 = "Level_1";
    private const string Gui = "GUI";
    
    void Start()
    {
        SceneManager.LoadScene(Player, LoadSceneMode.Additive);
        SceneManager.LoadScene(Level1, LoadSceneMode.Additive);
        SceneManager.LoadScene(Gui, LoadSceneMode.Additive);
    }
}
