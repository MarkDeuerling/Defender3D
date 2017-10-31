using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Game game;
    private const string Player = "Player";
    private const string Level1 = "Level_1";
    private const string Gui = "GUI";
    private const string GameOver = "GameOver";
    private const string MainCamera = "Camera";
    

    public static Game Singleton { get; private set; }
    
    public Player ThePlayer { get; set; }

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        LoadScene(MainCamera);
        LoadScene(Player);
        LoadScene(Level1);
        LoadScene(Gui);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.P))
            return;
        LoadScene("Pause");
        Time.timeScale = 0;
    }

    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    private void UnloadScene(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }

    public void OnPlayerDie()
    {
        UnloadScene(Player);
        UnloadScene(Gui);
        UnloadScene(Level1);
        LoadScene(GameOver);
        UnloadScene("Game");
    }
    
}
