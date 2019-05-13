using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaButton_L2 : MonoBehaviour
{

    public SpawnManager spawnerScript;
    public SceneManagerScript sceneManagerScript;

    private void Start()
    {
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("InteractionPlayer") && GameController.Instance.playerIsInteracting)
        {
            sceneManagerScript.buttonIsPressed = true;
            Debug.Log("Knappen tryckt");
            spawnerScript.GetComponent<SpawnManager>().InitializeSpawner();
            Destroy(gameObject);
         

        }
    }
}
