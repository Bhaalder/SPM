using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public bool buttonIsPressed;

   // public GameObject EnemySpawner;
    public SpawnManager spawnScript;
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
        if (other.gameObject.CompareTag("Player") && buttonIsPressed && Input.GetKeyDown(KeyCode.E) && spawnScript.isRoomCleared)
        {
            SceneManager.LoadScene("Level2WhiteBox");
        }
        if(!buttonIsPressed)
        {
            Debug.Log("Du måste låsa upp dörren!!");
        }
        if (!Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Klicka E!");
        }
        if (!spawnScript.isRoomCleared)
        {
            Debug.Log("Rensa rummet från fiender!!");
        }
    }
}
