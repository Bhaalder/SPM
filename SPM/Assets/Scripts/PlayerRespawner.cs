using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    private int HP = 100;
    private int AR = 100;
    public GameObject Player;
    public GameObject[] respawn;
    public GameController cntrl;


    void Start()
    {
        HP = cntrl.GetComponent<GameController>().playerHP;
        AR = cntrl.GetComponent<GameController>().playerArmor;
    }

    // Update is called once per frame
    void Update()
    {
        
       

    }


    public void RespawnMethod()
    {
        if (GameController.Instance.gameEventID == 2)
        {
            Player.transform.position = respawn[0].transform.position;
            resetStatus();
        }
        if (GameController.Instance.gameEventID == 4)
        {
            Player.transform.position = respawn[1].transform.position;
            resetStatus();
        }
        if (GameController.Instance.gameEventID == 6)
        {
            Player.transform.position = respawn[2].transform.position;
            resetStatus();
        }
    }
    public void resetStatus()
    {
        cntrl.GetComponent<GameController>().playerHP = HP;
        cntrl.GetComponent<GameController>().playerArmor = AR;
    }
}
