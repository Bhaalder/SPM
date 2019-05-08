using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    //Main author: Teo
    //Secondary author: Marcus Söderberg
    public bool buttonIsPressed;
    public GameObject menuController;

    // Start is called before the first frame update
    void Start()
    {
        buttonIsPressed = false;
        // SpawnManager spawnScript = GetComponent<SpawnManager>();
        //DontDestroyOnLoad(gameObject);
        menuController = GameObject.Find("MenuController");



    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartLevelOne()
    {
        SceneManager.LoadScene("Level1WhiteBoxArea");
    }

    public void ExitGame()
    {
        Application.Quit();
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("InteractionPlayer") && buttonIsPressed && GameController.Instance.playerIsInteracting && GameController.Instance.sceneCompleted)
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

    public void EndGameScreen()
    {
        if (SceneManager.GetActiveScene().name == "Level2WhiteBox")
        {
            menuController.GetComponent<MenuController>().EndGameActivate();
        }
    }
}
