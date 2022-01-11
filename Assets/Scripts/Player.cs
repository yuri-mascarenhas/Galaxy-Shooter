using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private float _fireRate;
    private float _nextFire;
    [SerializeField]
    private float _leftLimit;
    [SerializeField]
    private float _rightLimit;
    [SerializeField]
    private float _bottomLimit;
    [SerializeField]
    private float _topLimit;

    public bool canTripleShot;

    void Start()
    {
        _speed = 5.0f;
        _leftLimit = -9.6f;
        _rightLimit = 9.6f;
        _bottomLimit = -4.15f;
        _topLimit = 4.15f;
        _fireRate = 0.25f;
        _nextFire = 0.0f;
        transform.position = new Vector3(0, -4, 0);
        canTripleShot = false;
    }

    void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        // Method to correct/limit the player's movement
        MoveLimiter();
    }

    private void MoveLimiter()
    {
        if(transform.position.x < _leftLimit) transform.position = new Vector3(_rightLimit, transform.position.y, 0);
        else if(transform.position.x > _rightLimit) transform.position = new Vector3(_leftLimit, transform.position.y, 0);
        if(transform.position.y < _bottomLimit) transform.position = new Vector3(transform.position.x, _bottomLimit, 0);
        else if(transform.position.y > _topLimit) transform.position = new Vector3(transform.position.x, _topLimit, 0);
    }

    private void Shoot()
    {
        if(Time.time >= _nextFire)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
            {
                if (canTripleShot)
                {
                    Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
                } 
                else
                {
                    Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                }
                _nextFire = Time.time + _fireRate;
            }
        }
    }

    public void TripleShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }
}
