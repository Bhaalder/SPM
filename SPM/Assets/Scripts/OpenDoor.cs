using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    //Author: Patrik Ahlgren
    public GameObject theObject;
    public GameObject button;



    void Start()
    {
    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameController.Instance.playerIsInteracting)
        {
            theObject.SetActive(!theObject.activeSelf);
            button.SetActive(!button.activeSelf);
        }
    }
}
