using System.Collections.Generic;
using GameState;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public List<string> StartScenes;

    public const string PlayerHealthUpdate = "PlayerHealthUpdate";
    public const string ScoreUpdate = "ScoreUpdate";
    public const string SpecialUpdate = "SpecialUpdate";
    public const string PlayerBind = "PlayerBind";
    public const string Hit = "Hit";
    public const string Pause = "Pause";
    public const string GameOver = "GameOver";
    
    private IGameState currentState = new PlayerState();    
    private readonly EventContainer eventContainer = new EventContainer();
    private static Game game;
    
    public static GameObject Player { get; private set; }
        
    public static IGameState CurrentState
    {
        get { return game.currentState; }
        set { game.currentState = value; }
    }

    private void Awake()
    {
        game = this;
    }

    private void Start()
    {
        Setup();
        BindPlayer();
    }

    private void Setup()
    {
        StartScenes.ForEach(AddScene);
        eventContainer
            .AddEvent(PlayerHealthUpdate)
            .AddEvent(ScoreUpdate)
            .AddEvent(Hit)
            .AddEvent(SpecialUpdate)
            .AddEvent(PlayerBind);
    }

    private static void BindPlayer()
    {
        Bind(PlayerBind, OnPlayerBind);
    }
    
    private static void OnPlayerBind(GameObject entity)
    {
        Player = entity;
    }
    
    private void Update()
    {
        currentState.Update();
    }

    public static void AddScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    public static void UnLoadScene(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
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