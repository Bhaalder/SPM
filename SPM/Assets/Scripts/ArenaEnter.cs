using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnter : MonoBehaviour
{

    public GameObject objref;
    private Renderer rend1;
    private Collider coll1;
    
    // Start is called before the first frame update
    void Start()
    {
        rend1 = objref.GetComponent<Renderer>();
        coll1 = objref.GetComponent<Collider>();
        rend1.enabled = false;
        coll1.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            rend1.enabled = true;
            coll1.enabled = true;
        }
    }
}
