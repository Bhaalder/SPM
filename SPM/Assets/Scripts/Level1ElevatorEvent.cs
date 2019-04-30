﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1ElevatorEvent : MonoBehaviour
{

    public GameObject aDoor;
    public SpawnManager spawnerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && GameController.Instance.playerIsInteracting)
        {
            aDoor.SetActive(false);
            spawnerScript.GetComponent<SpawnManager>().InitializeSpawner();
            Destroy(gameObject);
        }
    }
}
