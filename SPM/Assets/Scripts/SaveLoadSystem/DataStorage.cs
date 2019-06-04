using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    public float Timer { get; set; }
    public int SceneBuildIndex { get; set; }
    public int CurrentCheckpoint { get; set; }
    public int KillCount { get; set; }

    [SerializeField] private GameObject Enemy1;
    [SerializeField] private GameObject Enemy3;
    [SerializeField] private GameObject Enemy4;

    public List<EnemyData> enemies = new List<EnemyData>();
    private List<SpawnerData> spawners = new List<SpawnerData>();

    public void SetData()
    {
        Timer = GameController.Instance.GetComponent<Timer>().GetFinalTime();
        SceneBuildIndex = GameObject.FindObjectOfType<SceneManagerScript>().SceneBuildIndex;
        CurrentCheckpoint = GameController.Instance.GameEventID;
    }

    public void LoadData()
    {
        LoadSpawnerData();
        LoadLevelData();
    }

    public void SaveSpawnerData(SpawnManager spawnManager)
    {
        SpawnerData data = new SpawnerData(spawnManager);
        spawners.Add(data);
    }

    public void InitializeSpawnerDataSave()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject target in gameObjects)
        {
            target.GetComponent<SpawnManager>().SaveSpawnerData();
        }
    }

    private void LoadSpawnerData()
    {
        GameObject[] spawnersingame = GameObject.FindGameObjectsWithTag("Spawner");

        spawners = SaveSystem.LoadSpawnerData();

        foreach (SpawnerData spawnerData in spawners)
        {
            int ID = spawnerData.SpawnerInstancedID;

            foreach(GameObject target in spawnersingame)
            {
                if (ID == target.GetInstanceID())
                {
                    spawnerData.TotalEnemiesInCurrentWave = target.GetComponent<SpawnManager>().TotalEnemiesInCurrentWave;
                    spawnerData.EnemiesInWaveLeft = target.GetComponent<SpawnManager>().EnemiesInWaveLeft;
                    spawnerData.SpawnedEnemies = target.GetComponent<SpawnManager>().SpawnedEnemies;
                    spawnerData.CurrentWave = target.GetComponent<SpawnManager>().CurrentWave;
                }
            }
        }
    }
    private void LoadLevelData()
    {
        LevelData data = SaveSystem.LoadLevelData();

        GameController.Instance.GetComponent<Timer>().SetTimer(data.Timer);
        GameObject.FindObjectOfType<SceneManagerScript>().SceneBuildIndex = SceneBuildIndex;
        GameController.Instance.GameEventID = data.CurrentCheckpoint;
        KillCount = data.KillCount;
    }


    public void SaveEnemyData()
    {
        SaveSystem.DeleteEnemySaveFile();
        enemies.Clear();

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject target in gameObjects)
        {
            target.GetComponent<Enemy>().SaveEnemyData();
        }

        SaveSystem.WriteEnemyDataToFile(enemies);
    }

    public void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        GameController.Instance.PlayerHP = data.PlayerHP;
        GameController.Instance.PlayerArmor = data.PlayerArmor;
        GameController.Instance.SlowmotionSlider.value = data.SlowmotionValue;

        Vector3 position;
        position.x = data.PlayerPosition[0];
        position.y = data.PlayerPosition[1];
        position.z = data.PlayerPosition[2];

        Vector3 rotation;
        rotation.x = data.PlayerRotation[0];
        rotation.y = data.PlayerRotation[1];
        rotation.z = data.PlayerRotation[2];

        GameController.Instance.Player.transform.position = position;
        GameController.Instance.Player.transform.eulerAngles = new Vector3(rotation.x, rotation.y, rotation.z);

        GameController.Instance.PlayerWeapons = data.PlayerWeapons;
        GameController.Instance.SelectedWeapon = data.SelectedWeapon;
        GameController.Instance.UpdateSelectedWeapon();
        GameController.Instance.Player.GetComponent<PlayerInput>().SwitchWeaponAnimation(SelectedWeapon);
    }

    public void LoadEnemyData()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject target in gameObjects)
        {
            Destroy(target);
        }

        enemies = SaveSystem.LoadEnemies();

        foreach (EnemyData enemyData in enemies)
        {
            string name = enemyData.EnemyName;
            if (name.Contains("Enemy1"))
            {
                Vector3 position = new Vector3(enemyData.EnemyPositionX, enemyData.EnemyPositionY, enemyData.EnemyPositionZ);
                Debug.Log(enemyData.EnemyName + " " + " " + enemyData.EnemyPositionX + " " + enemyData.EnemyPositionY + " " + enemyData.EnemyPositionZ);
                GameObject enemy = GameObject.Instantiate(GameController.Instance.Enemy1);
                enemy.GetComponent<Enemy>().agent.Warp(position);
                enemy.transform.rotation = Quaternion.Euler(enemyData.EnemyRotationX, enemyData.EnemyRotationY, enemyData.EnemyRotationZ);
                foreach (GameObject target in spawners)
                {
                    if (target.GetInstanceID() == enemyData.ParentID)
                    {
                        enemy.transform.parent = target.transform;
                    }
                }
                Debug.Log("Enemy is at location: " + enemy.transform.position);
            }
            else if (name.Contains("Enemy3"))
            {
                Vector3 position = new Vector3(enemyData.EnemyPositionX, enemyData.EnemyPositionY, enemyData.EnemyPositionZ);
                GameObject enemy = GameObject.Instantiate(GameController.Instance.Enemy3);
                enemy.GetComponent<Enemy>().agent.Warp(position);
                enemy.transform.rotation = Quaternion.Euler(enemyData.EnemyRotationX, enemyData.EnemyRotationY, enemyData.EnemyRotationZ);
                foreach (GameObject target in spawners)
                {
                    if (target.GetInstanceID() == enemyData.ParentID)
                    {
                        enemy.transform.parent = target.transform;
                    }
                }
                Debug.Log("Enemy is at location: " + enemy.transform.position);
            }
            else if (name.Contains("Enemy4"))
            {
                Vector3 position = new Vector3(enemyData.EnemyPositionX, enemyData.EnemyPositionY, enemyData.EnemyPositionZ);
                GameObject enemy = GameObject.Instantiate(GameController.Instance.Enemy4);
                enemy.GetComponent<Enemy>().agent.Warp(position);
                enemy.transform.rotation = Quaternion.Euler(enemyData.EnemyRotationX, enemyData.EnemyRotationY, enemyData.EnemyRotationZ);
                foreach (GameObject target in spawners)
                {
                    if (target.GetInstanceID() == enemyData.ParentID)
                    {
                        enemy.transform.parent = target.transform;
                    }
                }
                Debug.Log("Enemy is at location: " + enemy.transform.position);
            }
            else
            {
                Debug.LogError("Unknown Enemy Type. Instantiate failed for: " + name);
            }

        }
    }

    public void SaveGame()
    {
        GameController.Instance.SavePlayerData();
        SaveEnemyData();
    }

    public void LoadGame()
    {
        LoadPlayerData();
        LoadEnemyData();
    }
}
