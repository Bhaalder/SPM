using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsLvl1 : MonoBehaviour
{
    private bool isOpen;
    GameObject theDoor;
    Renderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("_Color");
        rend.material.SetColor("_Color", Color.black);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameController.Instance.playerIsInteracting && !isOpen)
        {
            
            theDoor.GetComponent<Renderer>().enabled = false;
            theDoor.GetComponent<Collider>().enabled = false;
            isOpen = true;
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.red);
        }
        if(other.gameObject.CompareTag("Player") && GameController.Instance.playerIsInteracting && isOpen)
        {
            theDoor.GetComponent<Renderer>().enabled = true;
            theDoor.GetComponent<Collider>().enabled = true;
            rend.material.SetColor("_Color", Color.black);

        }
    }

}
