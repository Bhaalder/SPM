using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Wave
{
    //Author: Marcus Söderberg
    public GameObject[] Enemies;
    public int[] Numberofenemy;
}

public class SpawnManager : MonoBehaviour
{
    //Author: Marcus Söderberg
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
    public bool isArenaSpawner;

    void Start()
    {
        _currentWave = -1; // avoid off by 1
        _totalWaves = Waves.Length - 1; // adjust, because we're using 0 index
        isRoomCleared = false;
        // StartNextWave();
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
        Debug.Log("Next wave started");

        _currentWave++;

        int[] enemycount;
        enemycount = null;

        enemycount = Waves[_currentWave].Numberofenemy;


        for (int i = 0; i < enemycount.Length; i++)
        {
            _totalEnemiesInCurrentWave += enemycount[i];

        }

        //_totalEnemiesInCurrentWave = Waves[_currentWave].EnemiesPerWave;


        Debug.Log("Total Enemies in Current Wave: " + _totalEnemiesInCurrentWave);
        _enemiesInWaveLeft = 0;
        _spawnedEnemies = 0;


        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn all of our enemies
    IEnumerator SpawnEnemies()
    {
        int enemiesCount = Waves[_currentWave].Enemies.Length;
        GameObject[] enemies = Waves[_currentWave].Enemies;

        while (_spawnedEnemies < _totalEnemiesInCurrentWave)
        {

            foreach (GameObject enemy in enemies)
            {
                int place = System.Array.IndexOf(enemies, enemy);
                Debug.Log("Place in array: " + place);
                int numberofenemytospawn = Waves[_currentWave].Numberofenemy[place];

                for (int i = 0; i < numberofenemytospawn; i++)
                {
                    _spawnedEnemies++;
                    _enemiesInWaveLeft++;
                    spawnPointIndex++;
                    Debug.Log("Creating enemy number: " + i);
                    var newEnemy1 = Instantiate(enemies[place], SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
                    newEnemy1.transform.parent = gameObject.transform;
                    if (spawnPointIndex == SpawnPoints.Length - 1) { spawnPointIndex = 0; }
                    yield return new WaitForSeconds(TimeBetweenEnemies);
                }
            }

            /*var newEnemy1 = Instantiate(enemy1, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
            newEnemy1.transform.parent = gameObject.transform;
            if (spawnPointIndex == SpawnPoints.Length - 1) { spawnPointIndex = 0; }    
            yield return new WaitForSeconds(TimeBetweenEnemies);*/

        }
        yield return null;
    }

    // called by an enemy when they're defeated
    public void EnemyDefeated()
    {
        _enemiesInWaveLeft--;
        Debug.Log("Enemy Dead");

        // We start the next wave once we have spawned and defeated them all
        if (_enemiesInWaveLeft == 0 && _spawnedEnemies == _totalEnemiesInCurrentWave)
        {

            // win condition
            if (_currentWave == _totalWaves && _enemiesInWaveLeft == 0 && _spawnedEnemies == _totalEnemiesInCurrentWave)
            {
                Debug.Log("clear condition has been reached");
                StopCoroutine(SpawnEnemies());
                if (isArenaSpawner)
                {
                    Debug.Log("Du har vunnit!!");
                    GameController.Instance.SceneCompletedSequence();
                }

                isRoomCleared = true;
                return;
            }
            else {
                _totalEnemiesInCurrentWave = 0;
                StartNextWave();
            }

        }
        
    }
}