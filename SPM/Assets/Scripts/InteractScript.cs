using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    public GameObject theObject;
    private Renderer rend1;
    private Collider coll1;
    void Start()
    {
        coll1 = theObject.GetComponent<Collider>();
        rend1 = theObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        







    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            coll1.enabled = false;
            rend1.enabled = false;
        }
    }
}
