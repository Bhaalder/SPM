using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public ArenaButton aB;
   // public GameObject EnemySpawner;
    public SpawnManager spawnScript;
    // Start is called before the first frame update
    void Start()
    {

        // SpawnManager spawnScript = GetComponent<SpawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
      
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && aB.ButtonPressed && Input.GetKeyDown(KeyCode.E) && spawnScript.isRoomCleared)
        {
            SceneManager.LoadScene("Level2WhiteBox");
        }
        if(!aB.ButtonPressed)
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
