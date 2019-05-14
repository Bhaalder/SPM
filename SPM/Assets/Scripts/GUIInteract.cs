using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIInteract : MonoBehaviour
{
    
	public Text interact;
    public LayerMask layerMask;

    public Transform dir;

    private float distanceToTarget = 2f;
	
    private void Awake(){
        interact = GameObject.Find("InteractionText").GetComponent<Text>();
        
    }

    private void Start() {
        dir = GameObject.Find("Player").transform.GetChild(2);
        dir.position -= new Vector3(0, 0, 0.1f);
    }

    private void LateUpdate() {
        Physics.Raycast(dir.position, Camera.main.transform.forward, out RaycastHit hit, distanceToTarget, layerMask);
        interact.enabled = false;
        try {
            if (hit.transform.gameObject.tag == "InteractableObject") {
                interact.enabled = true;
            }
        } catch (System.NullReferenceException) {

        }

        Debug.DrawLine(dir.position, hit.point, Color.green);
    }

}
