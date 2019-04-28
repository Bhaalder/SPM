using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp1 : MonoBehaviour
{
    public Transform powerUpSpawner;
    public float TimeToDestroy = 0.2f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().StartSpeedBoost(10, 10);
            GetComponentInParent<PowerUpSpawner>().Respawner();
            StartCoroutine(UsedBoost());   
        }
    }


    IEnumerator UsedBoost()
    {
        yield return new WaitForSeconds(TimeToDestroy);
        Destroy(gameObject);
    }
}
