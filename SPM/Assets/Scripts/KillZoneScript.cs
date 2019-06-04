﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneScript : MonoBehaviour
{
    //Author: Teo
    [SerializeField] private PlayerRespawner playerRespawner;
    [SerializeField] private Animator anim;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            StartCoroutine(FadeOutDie());
            GameController.Instance.GetComponent<Timer>().AddToTimer(15);
        }
        if (other.gameObject.CompareTag("Enemy")){
            other.GetComponent<Enemy>().InvokeDeath();
        }
    }

    private IEnumerator FadeOutDie() {
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        playerRespawner.RespawnMethod();
        anim.SetTrigger("FadeIn");
        yield return null;
    }

}
