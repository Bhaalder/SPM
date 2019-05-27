using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAmbience : MonoBehaviour
{
    public GameObject ting;
    // Start is called before the first frame update
    void Start()
    {
        AudioController.Instance.Play_InWorldspace("Space Ambience",ting);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
