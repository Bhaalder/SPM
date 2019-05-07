using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp1 : MonoBehaviour
{
    public Transform powerUpSpawner;
    public float speedIncrease = 0.1f;
    //public float TimeToDestroy = 0.0f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractionPlayer")
        {
            other.GetComponent<PlayerMovementController>().speedMultiplier = speedIncrease;
            GetComponentInParent<PowerUpSpawner>().Respawner();
            Destroy(gameObject);//
            //StartCoroutine(UsedBoost());
        }
    }


    //IEnumerator UsedBoost()
    //{
    //    yield return new WaitForSeconds(TimeToDestroy);
    //    Destroy(gameObject);
    //}
}
