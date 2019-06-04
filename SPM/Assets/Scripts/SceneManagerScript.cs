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
    public int SceneBuildIndex { get; set; }
    private DataStorage dataStorage;


    // Start is called before the first frame update
    void Start()
    {
        buttonIsPressed = false;
        // SpawnManager spawnScript = GetComponent<SpawnManager>();
        //DontDestroyOnLoad(gameObject);
        menuController = GameObject.Find("MenuController");
        SceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        dataStorage = FindObjectOfType<DataStorage>();
        dataStorage.LoadLastLevelData();
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

    public void StartLevelTwo() {
        SceneManager.LoadScene("Level2WhiteBox");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinuePreviousGame()
    {
        SceneManager.LoadScene(dataStorage.SceneBuildIndex);
    }

    public void EndGameScreen()
    {
        if (SceneManager.GetActiveScene().name == "Level2WhiteBox")
        {
            menuController.GetComponent<MenuController>().EndGameActivate();
        }
    }
}
