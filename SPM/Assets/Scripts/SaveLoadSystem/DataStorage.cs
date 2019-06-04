using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    public float Timer { get; set; }
    public int SceneBuildIndex { get; set; }
    public int CurrentCheckpoint { get; set; }
    public int KillCount { get; set; }

    private List<SpawnerData> spawners = new List<SpawnerData>();

    public void SetData()
    {
        Timer = GameController.Instance.GetComponent<Timer>().GetFinalTime();
        SceneBuildIndex = GameObject.FindObjectOfType<SceneManagerScript>().SceneBuildIndex;
        CurrentCheckpoint = GameController.Instance.GameEventID;
    }

    public void LoadData()
    {

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
}
