using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    public float mouseSensitivity = 10;
    public Transform cameraTarget, player;
    public float distanceFromTarget = 2.4f;
    
    public Vector2 cameraClamp = new Vector2(-40, 85);
    public LayerMask layerMask;
    
    float mouseX;
    float mouseY;

    private void Start() {
        player = GameObject.Find("Player").transform;
        cameraTarget = GameObject.Find("CameraTarget").transform;
    }

    private void LateUpdate() {
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

        //bool hitTarget = Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, 100f, layerMask);
        //if(hitTarget && hit.distance < distanceFromTarget) {
        //    distanceFromTarget = hit.distance;
        //}
        //if(hitTarget && hit.distance > distanceFromTarget) {
        //    distanceFromTarget = desiredDistanceFromTarget;
        //}
        transform.position = cameraTarget.position - transform.forward * distanceFromTarget;
    }

    private void ScrollWheelZoom() {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) {
            distanceFromTarget -= 0.15f; //zoom in
        } else if (scroll < 0f) {
            distanceFromTarget += 0.15f; //zoom out
        }
    }
}
