using UnityEngine;

public class Expand : MonoBehaviour
{
    public float Rate;
    public float DestroyTime;

    private void Start()
    {
        transform.SetParent(null);
        var pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, -14);
        Destroy(gameObject, DestroyTime);
    }

    private void Update()
    {
        GetComponent<MeshRenderer>().enabled = false;
        transform.localScale += Vector3.one * Rate * Time.deltaTime;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
