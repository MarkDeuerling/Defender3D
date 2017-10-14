using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Player PlayerPref;
    public CameraLogic CamPref;
    public Light LightPref;
    public Enemy EnemyPref;
    public Bullet BulletPref;
    public float SpawnRate;
    public Vector3 SpawnPosition;

    private Spawner spawner;

    void Start()
    {
        Instantiate<Light>(LightPref);

        var player = Instantiate<Player>(PlayerPref);
        player.Init(this);

        var camera = Instantiate<CameraLogic>(CamPref);
        camera.Init(this);

        spawner = new Spawner(this);
    }

    void Update()
    {
        spawner.Update();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(SpawnPosition, .5f);
    }
}
