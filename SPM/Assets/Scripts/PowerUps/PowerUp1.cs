using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp1 : MonoBehaviour
{
    //Author: Marcus Söderberg
    public Transform powerUpSpawner;
    private float speedIncrease = 1f;
    private float speedDuration = 2f;
    //public float TimeToDestroy = 0.0f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractionPlayer")
        {
            other.transform.parent.GetComponent<PlayerMovementController>().SpeedMultiplier(speedDuration, speedIncrease);       
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
