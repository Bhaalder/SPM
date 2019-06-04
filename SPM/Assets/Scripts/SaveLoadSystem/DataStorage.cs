using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private float currentCooldown;
    private float saveCooldown;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            LoadGameData();
        }

        saveCooldown = 5f;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            ContinousSave();
        }
    }

    #region PlayerData

    public void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if(data != null)
        {
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
            GameController.Instance.Player.transform.rotation = Quaternion.Euler(rotation);
            FindObjectOfType<FirstPersonCamera>().transform.rotation = Quaternion.Euler(rotation);
            FindObjectOfType<FirstPersonCamera>().gameObject.transform.rotation = Quaternion.Euler(rotation);

            GameController.Instance.PlayerWeapons = data.PlayerWeapons;
            GameController.Instance.SelectedWeapon = data.SelectedWeapon;
            GameController.Instance.UpdateSelectedWeapon();
            GameController.Instance.Player.GetComponent<PlayerInput>().SwitchWeaponAnimation(GameController.Instance.SelectedWeapon);
        }
    }
    #endregion;

    #region EnemyData
    public void SaveEnemyData()
    {
        SaveSystem.DeleteEnemySaveFile();
        try
        {
            enemies.Clear();
        }
        catch (System.Exception e)
        {
            Debug.Log("Could not clear DataStorage.enemies: " + e);
        }

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject target in gameObjects)
        {
            EnemyData data = new EnemyData(target.GetComponent<Enemy>().SaveEnemyData());
            enemies.Add(data);
            Debug.Log("Enemy Saved");
        }

        SaveSystem.WriteEnemyDataToFile(enemies);
    }

    public void LoadEnemyData()
    {
        enemies = SaveSystem.LoadEnemies();

        if (enemies != null)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
            foreach (GameObject target in gameObjects)
            {
                Destroy(target);
            }

            foreach (EnemyData enemyData in enemies)
            {
                string name = enemyData.EnemyName;
                if (name.Contains("Enemy1"))
                {
                    Vector3 position = new Vector3(enemyData.EnemyPositionX, enemyData.EnemyPositionY, enemyData.EnemyPositionZ);
                    GameObject enemy = GameObject.Instantiate(Enemy1);

                    foreach (GameObject target in spawners)
                    {
                        if (target.GetInstanceID() == enemyData.ParentID)
                        {
                            enemy.transform.parent = target.transform;
                        }
                    }
                    enemy.GetComponent<Enemy>().agent.Warp(position);
                    enemy.transform.rotation = Quaternion.Euler(enemyData.EnemyRotationX, enemyData.EnemyRotationY, enemyData.EnemyRotationZ);
                }
                else if (name.Contains("Enemy3"))
                {
                    Vector3 position = new Vector3(enemyData.EnemyPositionX, enemyData.EnemyPositionY, enemyData.EnemyPositionZ);
                    GameObject enemy = GameObject.Instantiate(Enemy3);

                    foreach (GameObject target in spawners)
                    {
                        if (target.GetInstanceID() == enemyData.ParentID)
                        {
                            enemy.transform.parent = target.transform;
                        }
                    }
                    enemy.GetComponent<Enemy>().agent.Warp(position);
                    enemy.transform.rotation = Quaternion.Euler(enemyData.EnemyRotationX, enemyData.EnemyRotationY, enemyData.EnemyRotationZ);
                }
                else if (name.Contains("Enemy4"))
                {
                    Vector3 position = new Vector3(enemyData.EnemyPositionX, enemyData.EnemyPositionY, enemyData.EnemyPositionZ);
                    GameObject enemy = GameObject.Instantiate(Enemy4);
                    foreach (GameObject target in spawners)
                    {
                        if (target.GetInstanceID() == enemyData.ParentID)
                        {
                            enemy.transform.parent = target.transform;
                        }
                    }
                    enemy.GetComponent<Enemy>().agent.Warp(position);
                    enemy.transform.rotation = Quaternion.Euler(enemyData.EnemyRotationX, enemyData.EnemyRotationY, enemyData.EnemyRotationZ);
                }
                else
                {
                    Debug.LogError("Unknown Enemy Type. Instantiate failed for: " + name);
                }

            }
        }
    }
    #endregion

    #region LevelData
    public void SaveLevelData()
    {
        Timer = GameController.Instance.GetComponent<Timer>().GetFinalTime();
        SceneBuildIndex = GameObject.FindObjectOfType<SceneManagerScript>().SceneBuildIndex;
        CurrentCheckpoint = GameController.Instance.GameEventID;
        KillCount = GameController.Instance.KillCount;
        SaveSystem.SaveLevelData(this);
    }
    public void LoadLevelData()
    {
        LevelData data = SaveSystem.LoadLevelData();

        if (data != null)
        {
            GameController.Instance.GetComponent<Timer>().SetTimer(data.Timer);
            SceneBuildIndex = data.SceneBuildIndex;
            GameController.Instance.GameEventID = data.CurrentCheckpoint;
            GameController.Instance.KillCount = data.KillCount;
        }
    }
    #endregion

    #region SpawnerData
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
        SaveSystem.SaveSpawnerData(spawners);
    }

    private void LoadSpawnerData()
    {
        GameObject[] spawnersingame = GameObject.FindGameObjectsWithTag("Spawner");

        spawners = SaveSystem.LoadSpawnerData();

        if(spawners != null)
        {
            foreach (SpawnerData spawnerData in spawners)
            {
                int ID = spawnerData.SpawnerInstancedID;

                foreach (GameObject target in spawnersingame)
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
    }
    #endregion


    public void SaveGame()
    {
        GameController.Instance.SavePlayerData();
        SaveEnemyData();
        InitializeSpawnerDataSave();
        SaveLevelData();
    }

    public void LoadGameData()
    {
        LoadLevelData();
        //LoadPlayerData();
        LoadEnemyData();
        LoadSpawnerData();        
    }

    public void LoadLastLevelData()
    {
        LevelData data = SaveSystem.LoadLevelData();
        if (data != null)
        {
            SceneBuildIndex = data.SceneBuildIndex;
        }
    }

    IEnumerator OnApplicationQuit()
    {
        SaveGame();
        yield return new WaitForSeconds(3f);
        Debug.Log("Game Exit");
    }

    private void ContinousSave()
    {
        currentCooldown -= Time.deltaTime;

        if (currentCooldown > 0)
        {
            return;
        }

        SaveGame();
        currentCooldown = saveCooldown;
    }
}
