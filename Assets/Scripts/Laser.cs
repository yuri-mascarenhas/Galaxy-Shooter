using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void Start()
    {
        speed = 10.0f;
    }

    void Update()
    {
        Move();
        if (transform.position.y >= 6.0f) Destroy(this.gameObject);
    }

    private void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
