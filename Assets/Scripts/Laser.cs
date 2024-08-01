using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;

    public GameObject explosionPf;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void Destroy()
    {
        Instantiate(explosionPf, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
