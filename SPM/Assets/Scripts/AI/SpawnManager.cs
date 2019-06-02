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
    [SerializeField] private Wave[] Waves; // class to hold information per wave
    [SerializeField] private Transform[] SpawnPoints;
    public float TimeBetweenEnemies = 2f;

    private int totalEnemiesInCurrentWave;
    private int enemiesInWaveLeft;
    private int spawnedEnemies;

    private int currentWave;
    private int totalWaves;
    private int spawnPointIndex = 0;


    // Designer input
    [SerializeField] private bool isRoomCleared;
    [SerializeField] private bool isArenaSpawner;
    [SerializeField] private bool isCommandoRoom;
    [SerializeField] private GameObject door;



    void Start()
    {
        currentWave = -1; // avoid off by 1
        totalWaves = Waves.Length - 1; // adjust, because we're using 0 index
        isRoomCleared = false;
        // StartNextWave(); //used for testing
        Debug.Log("This is my gameobject ID: " + gameObject.GetInstanceID());
    }


    public void InitializeSpawner()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        Debug.Log("Next wave started");

        currentWave++;

        int[] enemycount;
        enemycount = null;

        enemycount = Waves[currentWave].Numberofenemy;

        for (int i = 0; i < enemycount.Length; i++)
        {
            totalEnemiesInCurrentWave += enemycount[i];
        }

        enemiesInWaveLeft = 0;
        spawnedEnemies = 0;

        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn all of our enemies
    IEnumerator SpawnEnemies()
    {
        int enemiesCount = Waves[currentWave].Enemies.Length;
        GameObject[] enemies = Waves[currentWave].Enemies;

        while (spawnedEnemies < totalEnemiesInCurrentWave)
        {

            foreach (GameObject enemy in enemies)
            {
                int place = System.Array.IndexOf(enemies, enemy);
                int numberofenemytospawn = Waves[currentWave].Numberofenemy[place];

                for (int i = 0; i < numberofenemytospawn; i++)
                {
                    spawnedEnemies++;
                    enemiesInWaveLeft++;
                    spawnPointIndex++;

                    GameObject newEnemy1 = Instantiate(enemies[place], SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
                    newEnemy1.transform.parent = gameObject.transform;
                    try
                    {
                        newEnemy1.GetComponent<Enemy>().ParentID = gameObject.GetInstanceID();
                        Debug.Log(newEnemy1.GetComponent<Enemy>().ParentID);
                    }
                    catch (Exception e)
                    {
                        newEnemy1.GetComponent<Enemy>().ParentID = 0;
                        Debug.Log(newEnemy1.GetComponent<Enemy>().ParentID);
                    }
                    if (spawnPointIndex == SpawnPoints.Length - 1) { spawnPointIndex = 0; }
                    yield return new WaitForSeconds(TimeBetweenEnemies);
                }
            }

        }
        yield return null;
    }

    
    
    // called by an enemy when they're defeated
    public void EnemyDefeated()
    {
        enemiesInWaveLeft--;

        // We start the next wave once we have spawned and defeated them all
        if (enemiesInWaveLeft == 0 && spawnedEnemies == totalEnemiesInCurrentWave)
        {

            // Check to see if the last enemy was killed from the last wave
            if (currentWave == totalWaves && enemiesInWaveLeft == 0 && spawnedEnemies == totalEnemiesInCurrentWave)
            {
                Debug.Log("clear condition has been reached");
                StopCoroutine(SpawnEnemies());
                if (isArenaSpawner)
                {
                    GameController.Instance.SceneCompletedSequence(true);
                    AudioController.Instance.FadeOut("Song4Loop", 5, 0);
                    GameObject sceneManager = GameObject.Find("SceneManager");
                    sceneManager.GetComponent<SceneManagerScript>().EndGameScreen();
                    door.SetActive(false);
                }
                if (isCommandoRoom)
                {
                    door.SetActive(false);
                }

                isRoomCleared = true;
                return;
            }
            else {
                totalEnemiesInCurrentWave = 0;
                StartNextWave();
            }

        }
        
    }
}