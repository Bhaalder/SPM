using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    
    public GameObject Player;
    public GameObject[] respawn;
    //public GameController cntrl;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.playerHP < 1) {
            RespawnMethod();
        }
    }


    public void RespawnMethod()
    {
        if (GameController.Instance.gameEventID == 1)
        {
            Player.transform.position = respawn[0].transform.position;
            resetStatus();
        }
        if (GameController.Instance.gameEventID == 2)
        {
            Player.transform.position = respawn[1].transform.position;
            resetStatus();
        }
        if (GameController.Instance.gameEventID == 3)
        {
            Player.transform.position = respawn[2].transform.position;
            resetStatus();
        }
        if (GameController.Instance.gameEventID == 4)
        {
            Player.transform.position = respawn[3].transform.position;
            resetStatus();
        }
        if (GameController.Instance.gameEventID == 5)
        {
            Player.transform.position = respawn[4].transform.position;
            resetStatus();
        }
    }
    public void resetStatus()
    {
        GameController.Instance.playerHP = 100;
        GameController.Instance.playerArmor = 100;
    }
}
