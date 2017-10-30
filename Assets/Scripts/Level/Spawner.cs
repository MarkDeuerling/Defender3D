using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;
    public int Count;
    public int SpawnRate;

    private Timer timer;
    private int currentCount;

    private void Start()
    {
        timer = new Timer();
    }

    private void Update()
    {
        if (currentCount == Count)
            Destroy(gameObject);
        if (!timer.IsTimeUp(Time.deltaTime, SpawnRate)) 
            return;
        Instantiate(Enemy).transform.position = this.GetPosition();
        timer.Reset();
        currentCount++;
    }
}