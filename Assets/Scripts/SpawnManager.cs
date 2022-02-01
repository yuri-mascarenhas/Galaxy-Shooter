using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyAI _enemyPrefab;
    [SerializeField] private PowerUp[] _powerUpsPrefab;
    private Time _time;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
        SpawnPowerUps();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Controls the spawning of enemies
    private void SpawnEnemies()
    {
        StartCoroutine(EnemiesRoutine());
    }

    // Controls the spawning of powerups
    private void SpawnPowerUps()
    {
        StartCoroutine(PowerUpRoutine());
    }

    // Enemies Coroutine
    private IEnumerator EnemiesRoutine()
    {
        while (true)
        {
            Instantiate(_enemyPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }

    // PowerUp Coroutine
    private IEnumerator PowerUpRoutine()
    {
        while (true)
        {
            int id = Random.Range(0, 3);
            Instantiate(_powerUpsPrefab[id], new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
