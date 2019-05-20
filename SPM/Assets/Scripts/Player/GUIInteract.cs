using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIInteract : MonoBehaviour{

    //Author: Patrik Ahlgren
	public Text interactText;
    public LayerMask layerMask;

    public Transform dir;

    private float distanceToTarget = 2f;
	
    private void Awake(){
        interactText = GameObject.Find("InteractionText").GetComponent<Text>();       
    }

    private void Start() {
        dir = GameObject.Find("Player").transform.GetChild(2);
        dir.position -= new Vector3(0, 0, 0.1f);
    }

    private void LateUpdate() {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, distanceToTarget, layerMask);
        interactText.enabled = false;
        try {
            if (hit.transform.gameObject.tag == "InteractableObject") {
                interactText.enabled = true;
                if (GameController.Instance.playerIsInteracting) {
                    hit.transform.GetComponent<InteractableObject>().Interact();
                    GameController.Instance.playerIsInteracting = false;
                }
            }
        } catch (System.NullReferenceException) {

        }

        Debug.DrawLine(dir.position, hit.point, Color.green);
    }

}
