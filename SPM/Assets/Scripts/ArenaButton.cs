﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaButton : MonoBehaviour
{
    public SpawnManager spawnerScript;
    public SceneManagerScript sceneManagerScript;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            sceneManagerScript.buttonIsPressed = true;
            Debug.Log("Knappen tryckt");
            spawnerScript.GetComponent<SpawnManager>().InitializeSpawner();
            Destroy(gameObject);
        }
    }
}
