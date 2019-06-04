﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnerData
{

    public int SpawnerInstancedID { get; set; }
    public int TotalEnemiesInCurrentWave { get; set; }
    public int EnemiesInWaveLeft { get; set; }
    public int SpawnedEnemies { get; set; }
    public int CurrentWave { get; set; }

    public SpawnerData(SpawnManager spawnManagerData)
    {
        SpawnerInstancedID = spawnManagerData.SpawnerInstancedID;
        TotalEnemiesInCurrentWave = spawnManagerData.TotalEnemiesInCurrentWave;
        EnemiesInWaveLeft = spawnManagerData.EnemiesInWaveLeft;
        SpawnedEnemies = spawnManagerData.SpawnedEnemies;
        CurrentWave = spawnManagerData.CurrentWave;
    }
}