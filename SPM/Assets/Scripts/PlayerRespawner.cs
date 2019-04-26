using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    
    public GameObject Player;
    public GameObject[] respawn;
    public GameController cntrl;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       

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
    }
    public void resetStatus()
    {
        cntrl.GetComponent<GameController>().playerHP = 100;
        cntrl.GetComponent<GameController>().playerArmor = 100;
    }
}
