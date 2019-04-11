using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private LayerMask layerMasks;
    private Vector3 additionalMovement;
    public Vector3 GetAdditionalMovement() { return additionalMovement; }

    private RaycastHit groundInfo;
    private Ray groundRay;

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position - new Vector3(0, 1.1f, 0));
    }

    //Kontrollerar ifall spelaren kolliderar med något underfrån.
    public bool GetGroundCollision()
    {
        groundRay = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down);
        if (Physics.Raycast(groundRay, out groundInfo, 1.1f))
            return true;
        else
            return false;
    }

    //Kontrollerar ifall spelaren kolliderar med något ovanfrån.
    public bool GetHeadCollision()
    {
        groundRay = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down);
        if (Physics.Raycast(groundRay, out groundInfo, 1.1f))
            return false;
        else
            return true;
    }

    public Vector3 GetForward()
    {
        if (!GetGroundCollision())
            return transform.forward;

        groundRay = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down);
        Physics.Raycast(groundRay, out groundInfo, 1.1f);

        return Vector3.Cross(groundInfo.normal, -transform.right);
    }

    public Vector3 GetRight()
    {
        if (!GetGroundCollision())
            return transform.right;

        groundRay = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down);
        Physics.Raycast(groundRay, out groundInfo, 1.1f);

        return Vector3.Cross(groundInfo.normal, transform.forward);
    }

    public bool GetFrontColl()
    {
        return false;
    }

    public bool GetBackColl()
    {
        return false;
    }

    public bool GetLeftColl()
    {
        return false;
    }

    public bool GetRightColl()
    {
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Platform") && other.gameObject.GetComponentInParent<Elevator>().GetMoving())
            additionalMovement = other.gameObject.GetComponentInParent<Elevator>().GetMovementSpeed();
        else
            additionalMovement = new Vector3(0, 0, 0);
    }
}
