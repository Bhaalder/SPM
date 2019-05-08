using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    //Author: Patrik Ahlgren
    void Start()
    {
        DontDestroyOnLoad(gameObject); 
    }
}
