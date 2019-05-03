using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerFocusTransform;
    private Transform camTransform;

    private Camera cam;

    private bool blocked = false;
    private RaycastHit hitForward;
    private RaycastHit hitBackward;
    public float distance = 2.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    [SerializeField] private float sensitivityX = 1f;
    [SerializeField] private float sensitivityY = 1f;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }
    private void Update()
    {
        MoveFocusPoint();
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(1, 1f, -distance);
        Quaternion rotation = Quaternion.Euler(-currentX * sensitivityX, -currentY * sensitivityY, 0);
        camTransform.position = playerTransform.position + rotation * dir;
        camTransform.LookAt(playerFocusTransform.position);
        Debug.DrawLine(playerTransform.position, playerFocusTransform.position);
        Debug.DrawLine(playerTransform.position, transform.position);
        MoveCamera();
        CameraBlocked();
    }

    private void MoveCamera()
    {
        if (blocked && !hitBackward.transform.name.Equals("MainCamera"))
            distance = hitBackward.distance;
        else
            distance = 4;
    }

    private void CameraBlocked()
    {
        blocked = Physics.Raycast(new Ray(playerTransform.position, -transform.forward), out hitBackward, 4f);
    }

    private void MoveFocusPoint()
    {
        Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitForward);
        Vector3 temp = playerFocusTransform.position;
        if (hitForward.transform != null)
            if (!hitForward.transform.name.Equals("CameraFocus") && hitForward.distance > 4.5f)
                playerFocusTransform.GetComponent<CameraFocus>().SetDistance(hitForward.distance - 4);
            else
                playerFocusTransform.GetComponent<CameraFocus>().SetDistance(20);

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
