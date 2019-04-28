using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    public float jumpForce;
    public float forwardForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        other.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
        other.GetComponent<Rigidbody>().AddForce(Vector3.forward * forwardForce);
    }

}
