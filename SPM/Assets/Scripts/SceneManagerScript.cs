using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public bool buttonIsPressed;

    // Start is called before the first frame update
    void Start()
    {
        buttonIsPressed = false;
        // SpawnManager spawnScript = GetComponent<SpawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
      
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && buttonIsPressed && GameController.Instance.playerIsInteracting && GameController.Instance.sceneCompleted)
        {
            SceneManager.LoadScene("Level2WhiteBox");
            GameController.Instance.SceneNotCompletedSequence();
        }
        if(!buttonIsPressed)
        {
            Debug.Log("Du måste låsa upp dörren!!");
        }
        if (!GameController.Instance.playerIsInteracting)
        {
            Debug.Log("Klicka E!");
        }
        if (!GameController.Instance.sceneCompleted)
        {
            Debug.Log("Rensa rummet från fiender!!");
        }
    }
}
