using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KommandoRumWave : MonoBehaviour
{

    public SpawnManager eSpawner;
    public GameObject gO;

    private bool encounterStarted;

    private void Start()
    {
        gO.SetActive(false);
    }
    
    public void StartEncounter()
    {
        if (!encounterStarted)
        {
            gO.SetActive(true);
            StartCoroutine(WaitASec());
            encounterStarted = true;
        }
        
        
    }

    private IEnumerator WaitASec() {
        yield return new WaitForSeconds(2);
        AudioController.Instance.Play_ThenPlay("Song2Start", "Song2Loop");
        try {
            AudioController.Instance.FadeOut("LetsGetItOn", 2, 0);
        } catch (System.Exception) {

        }        
        eSpawner.InitializeSpawner();
    }

}
