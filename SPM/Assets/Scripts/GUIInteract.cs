using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIInteract : MonoBehaviour
{
    
	public Text interact;
    public LayerMask layerMask;

    private float distanceToTarget = 5f;
	
    private void Awake(){
        interact = GameObject.Find("E_to_Interact").GetComponent<Text>();
        interact.gameObject.SetActive(false);
    }

    private void LateUpdate() {
        Physics.Raycast(transform.position, Camera.main.transform.forward, out RaycastHit hit, distanceToTarget, layerMask);
        try {
            if (hit.transform.gameObject.tag == "InteractableObject") {
                
                interact.gameObject.SetActive(true);
            } else {
                interact.gameObject.SetActive(false);
            }
        } catch (System.NullReferenceException) {

        }

        Debug.DrawLine(transform.parent.position, hit.point, Color.red);
    }

}
