using System.Collections;
using System.Collections.Generic;
using GameState;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public List<string> StartScenes;
    public float TimeStop = 0.1f;

    public const string PlayerHealthUpdate = "PlayerHealthUpdate";
    public const string ScoreUpdate = "ScoreUpdate";
    public const string SpecialUpdate = "SpecialUpdate";
    public const string PlayerBind = "PlayerBind";
    public const string Hit = "Hit";
    public const string Pause = "Pause";
    public const string GameOver = "GameOver";
    public const string ChangeLevel = "ChangeLevel";
    public const string DestroyEnemy = "DestroyEnemy";
    
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
        BindHit();
    }

    private void Setup()
    {
        StartScenes.ForEach(AddScene);
        eventContainer
            .AddEvent(PlayerHealthUpdate)
            .AddEvent(ScoreUpdate)
            .AddEvent(Hit)
            .AddEvent(SpecialUpdate)
            .AddEvent(PlayerBind)
            .AddEvent(ChangeLevel)
            .AddEvent(DestroyEnemy);
    }

    private static void BindPlayer()
    {
        Bind(PlayerBind, OnPlayerBind);
    }

    private void BindHit()
    {
        Bind(Hit, OnHit);
    }

    private void OnHit(GameObject entity)
    {
        StartCoroutine(StopTime());
    }

    private IEnumerator StopTime()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(TimeStop);
        Time.timeScale = 1;
    }
    
    private void OnDestroy()
    {
        UnBindPlayer();
    }

    private static void UnBindPlayer()
    {
        Unbind(PlayerBind, OnPlayerBind);
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

    public static void UnloadScene(string scene)
    {
        game.Unload(scene);
    }

    private void Unload(string scene)
    {
        StartCoroutine(UnloadAsync(scene));
    }

    private static IEnumerator UnloadAsync(string scene)
    {
        var async = SceneManager.UnloadSceneAsync(scene);
        if (!async.isDone)
            yield return null;
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