using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Transform playerFocusTransform;

    private Camera cam;

    [SerializeField] private float distance = 20.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    [SerializeField] private float sensitivityX = 1f;
    [SerializeField] private float sensitivityY = 1f;

    private void Start()
    {
        playerFocusTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 dir = new Vector3(1, 0, distance);
        Quaternion rotation = Quaternion.Euler(currentX * sensitivityX, currentY * sensitivityY, 0);
        playerFocusTransform.position = playerTransform.position + rotation * dir;
        playerFocusTransform.LookAt(playerTransform.position);
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
