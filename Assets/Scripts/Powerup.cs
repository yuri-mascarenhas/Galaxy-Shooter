using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private int _typeID;

    void Update()
    {
        transform.Translate(Vector3.down * _speed *Time.deltaTime);
    }

    // Checks collision with the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null) player.PowerUpOn(_typeID);
            Destroy(this.gameObject);
        }
    }
}
