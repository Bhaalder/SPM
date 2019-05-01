﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFocus : MonoBehaviour
{
    public LayerMask layerMask;

    private void Start() {
    }

    void Update(){
        bool hitTarget = Physics.Raycast(transform.parent.position, transform.parent.forward, out RaycastHit hit, 10000f, layerMask);
        if (hitTarget) {
            transform.position = hit.point;
        }
        Debug.DrawLine(transform.parent.position, hit.point, Color.red);
    }
}