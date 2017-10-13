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

    void Start()
    {
        Instantiate<Light>(LightPref);

        var player = Instantiate<Player>(PlayerPref);
        player.Init(this);

        var camera = Instantiate<CameraLogic>(CamPref);
        camera.Init(this);

        var enemy = Instantiate<Enemy>(EnemyPref);
        enemy.Init(this);
    }
}
