using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KommandoRumWave : MonoBehaviour
{

    public SpawnManager eSpawner;
    public GameObject gO;


    private void Start()
    {
        gO.SetActive(false);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractionPlayer"))
        {
            eSpawner.InitializeSpawner();
            gO.SetActive(true);
            Destroy(gameObject);
        }
    }
}
