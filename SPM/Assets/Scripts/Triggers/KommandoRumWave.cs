using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KommandoRumWave : MonoBehaviour
{

    public SpawnManager eSpawner;
    public GameObject gO;
    private PopUpTrigger pTut;

    private bool encounterStarted;

    private void Start()
    {
        gO.SetActive(false);
        pTut = GameObject.Find("PopUpTrigger").GetComponent<PopUpTrigger>();
    }
    
    public void StartEncounter()
    {
        if (!encounterStarted)
        {
            gO.SetActive(true);
            StartCoroutine(WaitASec());
            pTut.PopUpMethod("ARENA COMBAT", "Careful, when disabling alien technology you will alert them!");
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
