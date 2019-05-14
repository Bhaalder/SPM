using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Teo
public class GUIInteract : MonoBehaviour
{
    
	public Text interact;
	
    void Start(){       
        interact.enabled = false;
    }
  
	public void OnTriggerStay(Collider other){
		if(other.gameObject.tag == "InteractableObject"){
		interact.enabled = true;
		} else {
        interact.enabled = false;
        }
	}
	//public void OnTriggerExit(Collider other){
	//	if(other.gameObject.tag == "InteractableObject"){
	//	interact.enabled = false;
	//	}
	//}
}
