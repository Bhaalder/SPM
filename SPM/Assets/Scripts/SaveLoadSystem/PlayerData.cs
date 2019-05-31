using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float [] PlayerPosition { get; set; }
    public float [] PlayerRotation { get; set; }
    public float PlayerHP { get; set; }
    public float PlayerArmor { get; set; }


    public PlayerData(GameController gameController)
    {
        PlayerHP = gameController.PlayerHP;
        PlayerArmor = gameController.PlayerArmor;
        PlayerPosition = new float[3];
        PlayerPosition[0] = gameController.Player.transform.position.x;
        PlayerPosition[1] = gameController.Player.transform.position.y;
        PlayerPosition[2] = gameController.Player.transform.position.z;
        PlayerRotation = new float[3];
        PlayerRotation[0] = gameController.Player.transform.eulerAngles.x;
        PlayerRotation[1] = gameController.Player.transform.eulerAngles.y;
        PlayerRotation[2] = gameController.Player.transform.eulerAngles.z;
    }
}
