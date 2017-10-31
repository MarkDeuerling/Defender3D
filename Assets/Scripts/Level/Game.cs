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
    private const string Pause = "Pause";
    // current level
    
    public static Game Singleton { get; private set; }
    
    public Player ThePlayer { get; set; }

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        AddScene(MainCamera);
        AddScene(Player);
        AddScene(Level1);
        AddScene(Gui);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.P))
            return;
        AddScene(Pause);
        Time.timeScale = 0;
    }

    private void AddScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OnGameOver()
    {
        LoadScene(GameOver);
    }
    
}
