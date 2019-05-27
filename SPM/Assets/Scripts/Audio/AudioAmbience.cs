using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAmbience : MonoBehaviour
{

    void Start()
    {
        AudioController.Instance.Play("Space Ambience");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
