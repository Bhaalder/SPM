using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public LayerMask layerMasks;
    private Vector3 additionalMovement;
    public Vector3 GetAdditionalMovement() { return additionalMovement; }

    private RaycastHit groundInfo;
    private Ray groundRay;

    //Returnerar om spelaren kolliderar med något underfrån.
    public bool GetGroundCollision()
    {
        groundRay = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down);
        if (Physics.Raycast(groundRay, out groundInfo, 1.1f, layerMasks))
            return true;
        else
            return false;
    }

    //Returnerar om spelaren kolliderar med något ovanfrån.
    public bool GetHeadCollision()
    {
        groundRay = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down);
        if (Physics.Raycast(groundRay, out groundInfo, 1.1f, layerMasks))
            return false;
        else
            return true;
    }


    //Returnerar markvinkeln för rörelse framåt
    public Vector3 GetForward()
    {
        if (!GetGroundCollision())
            return transform.forward;

        groundRay = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down);
        Physics.Raycast(groundRay, out groundInfo, 1.1f, layerMasks);

        return Vector3.Cross(groundInfo.normal, -transform.right);
    }

    //Returnerar markvinkeln för rörelse åt höger
    public Vector3 GetRight()
    {
        if (!GetGroundCollision())
            return transform.right;

        groundRay = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down);
        Physics.Raycast(groundRay, out groundInfo, 1.1f, layerMasks);

        return Vector3.Cross(groundInfo.normal, transform.forward);
    }

    public bool GetFrontColl()
    {
        Vector3 boxSize = new Vector3(0.49f, 0.3f, 0);
        if (Physics.BoxCast(transform.position + new Vector3(0, 0.6f, 0), boxSize, transform.forward, out RaycastHit boxHit))
        {
            return boxHit.distance < 0.51f;
        }
        else
            return false;     
    }

    public bool GetBackColl()
    {
        Vector3 boxSize = new Vector3(0.49f, 0.3f, 0);
        if (Physics.BoxCast(transform.position + new Vector3(0, 0.6f, 0), boxSize, -transform.forward, out RaycastHit boxHit))
            return boxHit.distance < 0.51f;
        else
            return false;
    }

    public bool GetLeftColl()
    {
        Vector3 boxSize = new Vector3(0.49f, 0.3f, 0);
        if (Physics.BoxCast(transform.position + new Vector3(0, 0.6f, 0), boxSize, -transform.right, out RaycastHit boxHit))
            return boxHit.distance < 0.51f;
        else
            return false;
    }

    public bool GetRightColl()
    {
        Vector3 boxSize = new Vector3(0, 0.3f, 0.49f);
        if (Physics.BoxCast(transform.position + new Vector3(0, 0.6f, 0), boxSize, transform.right, out RaycastHit boxHit))
        {
            return boxHit.distance < 0.51f;
        }
        else
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
