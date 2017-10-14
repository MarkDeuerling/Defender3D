using UnityEngine;

public class Spawner
{
    private Game game;
    private float time;
    private Enemy enemyPref;
    private float spawnRate;
    private Vector3 spawnPosition;

    public Spawner(Game game)
    {
        this.game = game;
        enemyPref = game.EnemyPref;
        spawnRate = game.SpawnRate;
        spawnPosition = game.SpawnPosition;
    }

    public void Update()
    {
        time += Time.deltaTime;
        if (time > spawnRate)
            Spawn();
    }

    void Spawn()
    {
        time = 0;
        var enemy = MonoBehaviour.Instantiate<Enemy>(enemyPref);
        enemy.Init(game);
        enemy.transform.position = spawnPosition;
    }
}
