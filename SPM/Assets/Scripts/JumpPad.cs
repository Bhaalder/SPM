using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    public float jumpForce;
    public float forwardForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractionPlayer")
        {
            other.transform.parent.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
            other.transform.parent.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * forwardForce);

        }
    }


}
    