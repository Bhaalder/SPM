using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public ArenaButton aB;
    public GameObject EnemySpawner;
    private SpawnManager spawnScript;
    // Start is called before the first frame update
    void Start()
    {

        SpawnManager spawnScript = GetComponent<SpawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
      
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && aB.ButtonPressed && Input.GetKeyDown(KeyCode.E) && spawnScript.isRoomCleared == true)
        {
            SceneManager.LoadScene("Level2WhiteBox");
        }
        else
        {
            Debug.Log("Du måste låsa upp dörren!!");
        }
    }
}
