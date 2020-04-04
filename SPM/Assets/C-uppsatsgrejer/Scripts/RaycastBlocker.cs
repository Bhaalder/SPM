using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBlocker : MonoBehaviour
{
    [SerializeField]bool isSpawner;
    [SerializeField]SpawnManager eSpawner;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isSpawner)
            {
                eSpawner.InitializeSpawner();
            }


            gameObject.SetActive(false);
        }
    }
}
