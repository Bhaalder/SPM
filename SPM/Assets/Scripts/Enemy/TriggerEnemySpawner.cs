using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemySpawner : MonoBehaviour
{
    public SpawnManager spawnerScript;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
      
            spawnerScript.GetComponent<SpawnManager>().InitializeSpawner();
            Destroy(gameObject);

        }

    }
}
