using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLevel1 : MonoBehaviour{

    void Start(){
        AudioController.Instance.Play_InWorldspace_WithTag("Button2", "SuperHuman");
    }


}
