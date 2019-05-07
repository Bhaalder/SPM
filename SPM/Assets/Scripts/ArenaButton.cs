using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaButton : MonoBehaviour
{
    public SpawnManager spawnerScript;
    public SceneManagerScript sceneManagerScript;

    public GameObject obj1;
    public GameObject obj2;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("InteractionPlayer") && GameController.Instance.playerIsInteracting)
        {
            sceneManagerScript.buttonIsPressed = true;
            Debug.Log("Knappen tryckt");
            spawnerScript.GetComponent<SpawnManager>().InitializeSpawner();
            Destroy(gameObject);
            obj1.SetActive(false);
            obj2.SetActive(false);





        }
    }
}
