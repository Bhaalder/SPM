using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject theObject;
    public GameObject button;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameController.Instance.playerIsInteracting)
        {
            theObject.SetActive(!theObject.activeSelf);
            button.SetActive(!button.activeSelf);
        }        
    }
}
