using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAmbience : MonoBehaviour
{

    void Start()
    {
        AudioController.Instance.Play("Space Ambience");
        AudioController.Instance.Play_Delay("RandomAmbience", 15f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
