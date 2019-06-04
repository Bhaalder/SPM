using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemySpawner : MonoBehaviour
{
    //Author: Teo
    public SpawnManager Hangarspawner;
    public SpawnManager Storagespawner;
    public SpawnManager hexagonspawner;
    public GameObject hangarDoor;

    [SerializeField] bool hangar;
    [SerializeField] bool storage;
    [SerializeField] bool hexagon;

    private bool triggered;
    //public GameObject storageDoor;

    private void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.CompareTag("InteractionPlayer"))
        {


            if (hangar && !triggered)
            {

                WaitASecHangar();
                hangarDoor.SetActive(true);
                triggered = true;
            }

            if (storage && !triggered)
            {
                WaitASecStorage();
                triggered = true;
            }
            if(hexagon && !triggered)
            {
                hexagonspawner.InitializeSpawner();
                triggered = true;
            }

                Destroy(gameObject);

        }

    }
    private IEnumerator WaitASecHangar()
    {
        yield return new WaitForSeconds(2);
        AudioController.Instance.Play_ThenPlay("Song2Start", "Song2Loop");
        Hangarspawner.InitializeSpawner();
    }
    private IEnumerator WaitASecStorage()
    {
        yield return new WaitForSeconds(2);
        AudioController.Instance.Play_ThenPlay("Song2Start", "Song2Loop");
        Storagespawner.InitializeSpawner();
    }
}

