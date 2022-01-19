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
        if (transform.position.y >= 6.0f)
        {
            if(transform.parent != null) Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
