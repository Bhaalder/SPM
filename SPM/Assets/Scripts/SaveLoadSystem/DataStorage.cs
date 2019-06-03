using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    public float Timer { get; set; }
    public int SceneBuildIndex { get; set; }
    public int CurrentCheckpoint { get; set; }
    public int KillCount { get; set; }

    public void SetData()
    {
        Timer = GameController.Instance.GetComponent<Timer>().GetTimer();
        SceneBuildIndex = GameObject.FindObjectOfType<SceneManagerScript>().SceneBuildIndex;
        CurrentCheckpoint = GameController.Instance.GameEventID;
    }

    public void LoadData()
    {

    }
}
