﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4AnimController : MonoBehaviour
{
    Animator anim;
    float speed;
    float direction;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        speed = Input.GetAxis("Vertical");

        anim.SetFloat("Speed", speed);
        anim.SetFloat("Direction", direction);


    }

    
}
