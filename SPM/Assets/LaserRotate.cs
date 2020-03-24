using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRotate : MonoBehaviour
{
    public float speed = 20f;
    
    

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
   
}
