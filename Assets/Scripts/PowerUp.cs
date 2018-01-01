using Statics;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float MoveSpeed;
    public float DestroyTime;
    public GameObject DestroyPref;

    private void Start()
    {
        Game.Bind(Game.BossDied, OnBossDie);
        Destroy(gameObject, DestroyTime);
    }

    private void OnDestroy()
    {
        Game.Unbind(Game.BossDied, OnBossDie);
    }

    private void OnBossDie(GameObject entity)
    {
        Instantiate(DestroyPref, this.GetPosition(), Quaternion.identity);
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.position += Vector3.right * MoveSpeed * Time.deltaTime;
    }
}
