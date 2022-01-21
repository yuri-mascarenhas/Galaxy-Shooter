using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _ExplosionPrefab;


    // Start is called before the first frame update
    void Start()
    {
        _speed = 1.65f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(transform.position.y < -6.5f)
        {
            float randomX = Random.Range(-7.7f, 7.7f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    // Controls the enemy movement
    private void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    // Detect and handle collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null) player.Damage(1);
            Instantiate(_ExplosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(other.tag == "Laser")
        {
            if(other.transform.parent != null) Destroy(other.transform.parent.gameObject);
            Destroy(other.gameObject);
            Instantiate(_ExplosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
