using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public float[] EnemyPosition { get; set; }
    public float[] EnemyRotation { get; set; }
    public float EnemyHealth { get; set; }
    public float PlayerArmor { get; set; }


    public EnemyData(Enemy enemy)
    {
        EnemyHealth = enemy.health;

        EnemyPosition = new float[3];
        EnemyPosition[0] = enemy.transform.position.x;
        EnemyPosition[1] = enemy.transform.position.y;
        EnemyPosition[2] = enemy.transform.position.z;

        EnemyRotation = new float[3];
        EnemyRotation[0] = enemy.transform.eulerAngles.x;
        EnemyRotation[1] = enemy.transform.eulerAngles.y;
        EnemyRotation[2] = enemy.transform.eulerAngles.z;
    }
}
