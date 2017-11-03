using System.CodeDom;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public List<string> StartScenes;

    public const string OnPlayer = "OnPlayer";

    private const string GameOver = "GameOver";
    private const string Pause = "Pause";
    private readonly EventContainer eventContainer = new EventContainer();
    private static Game game;
    
    private void Awake()
    {
        game = this;
    }

    private void Start()
    {
        Setup();
        eventContainer.AddEvent(OnPlayer);
    }

    private void Setup()
    {
        StartScenes.ForEach(AddScene);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.P))
            return;
        AddScene(Pause);
        Time.timeScale = 0;
    }

    private static void AddScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    private static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static void OnGameOver()
    {
        LoadScene(GameOver);
    }

    public static void Bind(string name, EventContainer.CallBack callback)
    {
        game.eventContainer.Bind(name, callback);
    }

    public static void Unbind(string name, EventContainer.CallBack callBack)
    {
        game.eventContainer.Unbind(name, callBack);
    }

    public static void Execute(string name, GameObject entity)
    {
        game.eventContainer.Execute(name, entity);
    }

}
