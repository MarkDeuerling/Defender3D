using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 1;
    public GameObject Bullet;
    public float FireRate;
    public Vector3 Offset;
    public float MoveSpeed;
    public float Amplitude;

    private Rigidbody body;
    private Timer timer;
    private float seed;

    private void Start()
    {
        body = this.GetRigidBody();
        timer = new Timer();
        seed = Random.value;
    }

    private void Update()
    {
        HealthCondition();
        Shoot();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider entity)
    {
        if (entity.HasNot(Tag.Bullet))
            return;
        Health--;
        Destroy(entity.gameObject);
    }

    private void HealthCondition()
    {
        if (Health <= 0)
            Destroy(gameObject);
    }

    private void Shoot()
    {
        if (!timer.IsTimeUp(Time.deltaTime, FireRate))
            return;
        Instantiate(Bullet).transform.position = this.GetPosition() + Offset;
        timer.Reset();
    }

    private void Move()
    {
//        var velocity = Vector3.left * MoveSpeed;
        var velocity = 
            new Vector3(-MoveSpeed,Mathf.Sin(MoveSpeed*Time.time*seed) * Amplitude,0);
        
        body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
    }
    
}
