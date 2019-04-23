using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneScript : MonoBehaviour
{

    public GameObject Player1;
    public GameObject respawn;
    public GameController cntrl;

    private int HP;
    private int AR;


    // Start is called before the first frame update
    void Start()
    {
        HP = cntrl.GetComponent<GameController>().playerHP;
        AR = cntrl.GetComponent<GameController>().playerArmor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player1.transform.position = respawn.transform.position;
            HP = 100;
            cntrl.GetComponent<GameController>().playerHP = HP;
            AR = 100;
            cntrl.GetComponent<GameController>().playerArmor = AR; 
        }
    }
}
