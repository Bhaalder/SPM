using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera: MonoBehaviour {
    //Author: Patrik Ahlgren

    public float MouseSensitivity = 60;
    [SerializeField] private Transform cameraTarget, player;
    [SerializeField] private Vector2 cameraClamp = new Vector2(-85, 85);
    [SerializeField] private Transform weaponCamera;


    private float mouseX;
    private float mouseY;
    private Vector3 targetRotation;
    private Vector3 velocity = Vector3.zero;

    private void Start() {
        player = GameObject.Find("Player").transform;
        cameraTarget = GameObject.Find("CameraTarget").transform;
        weaponCamera = transform.GetChild(0).GetChild(0);
    }

    private void Update() {
        if (GameController.Instance.GameIsPaused) {

        } else {
            CameraControl();
        }

    }

    private void CameraControl() {
        if (GameController.Instance.GameIsPaused) {

        } else {
            mouseX += Input.GetAxis("Mouse X") * MouseSensitivity * Time.unscaledDeltaTime;
            mouseY -= Input.GetAxis("Mouse Y") * MouseSensitivity * Time.unscaledDeltaTime;
            //foreach (Transform weapon in weaponCamera) {

            //    weapon.transform.position = Vector3.SmoothDamp(weapon.transform.position, transform.position, ref velocity, 1);
                
            //}
        }
        mouseY = Mathf.Clamp(mouseY, cameraClamp.x, cameraClamp.y);

        targetRotation = new Vector3(mouseY, mouseX);
        transform.eulerAngles = targetRotation;
        player.rotation = Quaternion.Euler(0, mouseX, 0);
        transform.position = cameraTarget.position;
    }
}
