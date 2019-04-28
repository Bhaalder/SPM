using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Wave
{
    public int EnemiesPerWave;
    public GameObject Enemy1;
}

public class SpawnManager : MonoBehaviour
{
    public Wave[] Waves; // class to hold information per wave
    public Transform[] SpawnPoints;
    public float TimeBetweenEnemies = 2f;

    private int _totalEnemiesInCurrentWave;
    private int _enemiesInWaveLeft;
    private int _spawnedEnemies;

    private int _currentWave;
    private int _totalWaves;
    private int spawnPointIndex = 0;
    public bool isRoomCleared;

    void Start()
    {
        /*_currentWave = -1; // avoid off by 1
        _totalWaves = Waves.Length - 1; // adjust, because we're using 0 index
        isRoomCleared = false;
        StartNextWave();*/
    }


    public void InitializeSpawner()
    {
        _currentWave = -1; // avoid off by 1
        _totalWaves = Waves.Length - 1; // adjust, because we're using 0 index
        isRoomCleared = false;
        StartNextWave();
    }

    void StartNextWave()
    {
        _currentWave++;

       

        _totalEnemiesInCurrentWave = Waves[_currentWave].EnemiesPerWave;
        _enemiesInWaveLeft = 0;
        _spawnedEnemies = 0;
        // win
        if (_currentWave == _totalWaves && _enemiesInWaveLeft == 0 && _enemiesInWaveLeft == 0 && _spawnedEnemies == _totalEnemiesInCurrentWave)
        {
            StopCoroutine(SpawnEnemies());
            Debug.Log("Du har vunnit!!");
            isRoomCleared = true;
            return;
        }

        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn all of our enemies
    IEnumerator SpawnEnemies()
    {
        GameObject enemy1 = Waves[_currentWave].Enemy1;
        while (_spawnedEnemies < _totalEnemiesInCurrentWave)
        {
            _spawnedEnemies++;
            _enemiesInWaveLeft++;
            spawnPointIndex++;

            //Random.Range(0, SpawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            var newEnemy1 = Instantiate(enemy1, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
            newEnemy1.transform.parent = gameObject.transform;
            if (spawnPointIndex == SpawnPoints.Length - 1) { spawnPointIndex = 0; }    
            yield return new WaitForSeconds(TimeBetweenEnemies);
            
        }
        yield return null;
    }

    // called by an enemy when they're defeated
    public void EnemyDefeated()
    {
        _enemiesInWaveLeft--;

        // We start the next wave once we have spawned and defeated them all
        if (_enemiesInWaveLeft == 0 && _spawnedEnemies == _totalEnemiesInCurrentWave)
        {
            StartNextWave();
        }
        
    }
}