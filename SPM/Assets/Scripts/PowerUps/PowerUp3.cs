using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp3 : MonoBehaviour
{
    public Transform powerUpSpawner;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("GameController").GetComponent<GameController>().playerArmor = 100;
            GameObject.Find("Powerup_Armor_Spawn").GetComponent<PowerUpSpawner>().Respawner(1);

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
