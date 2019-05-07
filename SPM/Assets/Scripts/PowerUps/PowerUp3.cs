﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp3 : MonoBehaviour
{
    public Transform powerUpSpawner;
    public float TimeToDestroy = 0.0f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractionPlayer")
        {
            GameController.Instance.GetComponent<GameController>().playerArmor = 100;
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
