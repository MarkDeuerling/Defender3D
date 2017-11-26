using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float MoveSpeed;
    public float DestroyTime;

    private void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

    private void Update()
    {
        transform.position += Vector3.right * MoveSpeed * Time.deltaTime;
    }
}
