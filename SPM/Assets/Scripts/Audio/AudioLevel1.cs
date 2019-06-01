using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioLevel1 : MonoBehaviour{

    void Start(){
        //AudioController.Instance.Play("RandomAmbience");
        AudioController.Instance.Play_InWorldspace_WithTag("LabBubbling", "SuperHuman");
    }
    private void Update()
    {
        
    }




}
