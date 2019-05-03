﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    public float mouseSensitivity = 10;
    public Transform cameraTarget, player;
    
        
    public Vector2 cameraClamp = new Vector2(-40, 85);
    
    private float mouseX;
    private float mouseY;
    
    [Header("Collision Speed")]
    public float moveSpeed = 40;
    public float wallSink = 0.3f;

    [Header("Distances")]
    public float distanceFromTarget = 2.4f;
    //public float closestDistanceToPlayer = 0.5f;

    [Header("Mask")]
    public LayerMask collisionMask;
    
    private void Start() {
        player = GameObject.Find("Player").transform;
        cameraTarget = GameObject.Find("CameraTarget").transform;
    }

    private void Update() {
        CameraControl();
        ScrollWheelZoom();
    }

    private void CameraControl() {
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        mouseY = Mathf.Clamp(mouseY, cameraClamp.x, cameraClamp.y);

        Vector3 targetRotation = new Vector3(mouseY, mouseX);
        transform.eulerAngles = targetRotation;
        player.rotation = Quaternion.Euler(0, mouseX, 0);

        CollisionCheck(cameraTarget.position - transform.forward * distanceFromTarget);
    }

    private void ScrollWheelZoom() {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) {
            distanceFromTarget -= 0.15f; //zoom in
        } else if (scroll < 0f) {
            distanceFromTarget += 0.15f; //zoom out
        }
    }

    private void CollisionCheck(Vector3 returnPoint) {
        RaycastHit hit;

        if (Physics.Linecast(cameraTarget.position, returnPoint, out hit, collisionMask)) {
            Vector3 normal = hit.normal * wallSink;
            Vector3 point = hit.point - normal;

            transform.position = Vector3.Lerp(transform.position, point, moveSpeed * Time.deltaTime);
        } else {
            transform.position = cameraTarget.position - transform.forward * distanceFromTarget;
        }
    }
}
