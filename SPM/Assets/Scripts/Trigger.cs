using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private bool triggered;
    public GameObject Player;
    
    public bool GetTriggered() { return triggered; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
            Player.transform.parent = transform;
        }
        Debug.Log("Trigger on");
        if (other.CompareTag("Panel"))
        {
            triggered = true;
            other.gameObject.transform.parent = transform;
        }
        Debug.Log("Trigger on");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = false;
            Player.transform.parent = null;
        }
        Debug.Log("Trigger off");
    }
}
