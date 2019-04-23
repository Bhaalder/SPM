using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    public GameObject theObject;
    private Renderer rend1;
    private Collider coll1;
    private bool isOpen;
    void Start()
    {
        coll1 = theObject.GetComponent<Collider>();
        rend1 = theObject.GetComponent<Renderer>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        







    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            coll1.enabled = false;
            rend1.enabled = false;
            isOpen = true;
        }else if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && isOpen)
        {
            coll1.enabled = true;
            rend1.enabled = true;
            isOpen = false;
        }
        
    }
}
