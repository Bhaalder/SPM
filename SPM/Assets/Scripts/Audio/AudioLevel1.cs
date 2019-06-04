using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioLevel1 : MonoBehaviour{
    [SerializeField] bool barSign;
    void Start(){
        //AudioController.Instance.Play("RandomAmbience");
        AudioController.Instance.Play_InWorldspace_WithTag("LabBubbling", "SuperHuman");

        if (barSign)
        {
            AudioController.Instance.Play_InWorldspace("BarSign", gameObject);
            AudioController.Instance.Play_InWorldspace("BarSignFlick", gameObject);
        }

    }
    private void Update()
    {
        
    }




}
