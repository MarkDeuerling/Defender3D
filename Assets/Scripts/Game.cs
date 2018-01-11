using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameState;
using Level;
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
    public const string Win = "Win";
    public const string ChangeLevel = "ChangeLevel";
    public const string DestroyEnemy = "DestroyEnemy";
    public const string BossLaser = "BossLaser";
    public const string PlayerDie = "PlayerDie";
    public const string BossDied = "BossDie";
    public const string BossSpawn = "BossSpawn";
    public const string GameStart = "GameStart";
    
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
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
    }

    private IEnumerator Start()
    {
        Setup();
        BindPlayer();
        BindHit();
        Bind(BossDied, OnBossDie);
        yield return null;
        SceneManager
            .GetSceneByName("Environment")
            .GetRootGameObjects().ToList()
            .ForEach(e =>
            {
                var env = e.GetComponent<Environment>();
                if (env)
                    env.enabled = true;
            });
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
            .AddEvent(DestroyEnemy)
            .AddEvent(BossLaser)
            .AddEvent(PlayerDie)
            .AddEvent(BossDied)
            .AddEvent(BossSpawn);
    }

    private void BindHit()
    {
        Bind(Hit, OnHit);
    }

    private void UnBindHit()
    {
        Unbind(Hit, OnHit);
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
        UnBindHit();
        Unbind(BossDied, OnBossDie);
        StopAllCoroutines();
    }
    
    private void BindPlayer()
    {
        Bind(PlayerBind, OnPlayerBind);
        Bind(PlayerDie, OnPlayerDie);
    }

    private void UnBindPlayer()
    {
        Unbind(PlayerBind, OnPlayerBind);
        Unbind(PlayerDie, OnPlayerDie);
    }
    
    private static void OnPlayerBind(GameObject entity)
    {
        Player = entity;
    }

    private void OnBossDie(GameObject entity)
    {
        StartCoroutine(BossDie());
    }

    private static IEnumerator BossDie()
    {
        yield return new WaitForSeconds(2f);
        LoadScene(Win);
    }

    private void OnPlayerDie(GameObject entity)
    {
        StartCoroutine(Die());
    }

    private static IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        LoadScene(GameOver);
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
        SceneManager.UnloadSceneAsync(scene);
//        game.Unload(scene);
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