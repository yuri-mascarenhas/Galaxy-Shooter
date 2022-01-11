using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot;
    public bool isSpeedBoostActive;
    public bool canShield;

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
    [SerializeField]
    private float _speedBoost;

    void Start()
    {
        _speed = 5.0f;
        _leftLimit = -9.6f;
        _rightLimit = 9.6f;
        _bottomLimit = -4.15f;
        _topLimit = 4.15f;
        _fireRate = 0.25f;
        _nextFire = 0.0f;
        _speedBoost = 2.0f;
        transform.position = new Vector3(0, -4, 0);
        canTripleShot = false;
        isSpeedBoostActive = false;
        canShield = false;
    }

    void Update()
    {
        Move();

        if (Time.time >= _nextFire)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Shoot();
                _nextFire = Time.time + _fireRate;
            }
        }
    }

    // Controls player's movement direction
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

    // Limit player's movement
    private void MoveLimiter()
    {
        if(transform.position.x < _leftLimit) transform.position = new Vector3(_rightLimit, transform.position.y, 0);
        else if(transform.position.x > _rightLimit) transform.position = new Vector3(_leftLimit, transform.position.y, 0);
        if(transform.position.y < _bottomLimit) transform.position = new Vector3(transform.position.x, _bottomLimit, 0);
        else if(transform.position.y > _topLimit) transform.position = new Vector3(transform.position.x, _topLimit, 0);
    }

    // Controls player's shooting action and type
    private void Shoot()
    {
        if (canTripleShot)
        {
            Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }

    // Activate PowerUp based on ID (0: triple shot, 1: speed boost, 2: shield) and start cooldown coroutine
    public void PowerUpOn(int powerID)
    {
        if (powerID == 0) canTripleShot = true;
        else if (powerID == 1) 
        { 
            isSpeedBoostActive = true;
            _speed *= _speedBoost;
        }
        else canShield = true;
        StartCoroutine(PowerDownRoutine(powerID));
    }

    // Coroutine that acts as a cooldown controler for the PowerUps
    public IEnumerator PowerDownRoutine(int powerID)
    {
        if(powerID == 0)
        {
            yield return new WaitForSeconds(5.0f);
            canTripleShot = false;
        }
        else if(powerID == 1)
        {
            yield return new WaitForSeconds(3.5f);
            _speed /= _speedBoost;
            isSpeedBoostActive = false;
        }
        else
        {
            yield return new WaitForSeconds(4.0f);
            canShield = false;
        }
        
    }
}
