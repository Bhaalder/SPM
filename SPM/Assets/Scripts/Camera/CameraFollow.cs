using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerFocusTransform;
    private Transform camTransform;

    private Camera cam;

    private float distance = 4.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    [SerializeField] private float sensitivityX = 1f;
    [SerializeField] private float sensitivityY = 1f;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(1, 1f, -distance);
        Quaternion rotation = Quaternion.Euler(-currentX * sensitivityX, -currentY * sensitivityY, 0);
        camTransform.position = playerTransform.position + rotation * dir;
        camTransform.LookAt(playerFocusTransform.position);
    }

    public void SetCurrentX(float x)
    {
        currentX = x;
    }

    public void SetCurrentY(float y)
    {
        currentY = y;
    }
}
