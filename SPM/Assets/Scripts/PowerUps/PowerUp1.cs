using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp1 : MonoBehaviour
{
    public Transform powerUpSpawner;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().StartSpeedBoost(10, 10);
            print(Time.time);
            GameObject.Find("Powerup_MoveSpeed_Spawn").GetComponent<PowerUpSpawner>().Respawner(1);
            //powerUpSpawner.GetComponent<PowerUpSpawner>().Respawner();
            print(Time.time);

            StartCoroutine(UsedBoost());
            
        }
    }


    IEnumerator UsedBoost()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        //gameObject.SetActive(false);
        
    }
}
