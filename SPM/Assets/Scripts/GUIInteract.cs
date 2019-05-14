using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Teo
public class GUIInteract : MonoBehaviour
{
    
	public Text interact;
	
	// Start is called before the first frame update
    void Start()
    {
        interact.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void OnTriggerEnter(Collider other){
		if(CompareTag("InteractableObject")){
		interact.enabled = true;
		}
	}
	public void OnTriggerExit(Collider other){
		if(CompareTag("InteractableObject")){
		interact.enabled = false;
		}
	}
}
