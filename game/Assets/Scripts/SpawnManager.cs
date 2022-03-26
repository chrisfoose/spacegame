using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        
    }
 
void update()
    {

    }

    //spawn game objects every 5 seconds
    //Create a coroutine of type IEnumerator -- Yield Events
    //while loop

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
           Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
           GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
           yield return new WaitForSeconds(5.0f);
        }
        //while loop (infinite loop)
            //Instantiate enemy prefab
            //yield wait for 5 seconds
    }

    IEnumerator SpawnPowerupRoutine()
    {
        //every 3-7 seconds, spawn in a powerup

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 2);
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            //GameObject newPowerup = Instantiate(_tripleShotPowerupPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}