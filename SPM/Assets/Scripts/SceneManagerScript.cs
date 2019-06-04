using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    //Main author: Teo
    //Secondary author: Marcus Söderberg
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    public bool buttonIsPressed { get; set; }
    public GameObject menuController;
    public int SceneBuildIndex { get; set; }
    private DataStorage dataStorage;
    private int level1;
    private int level2;
    private int mainMenuIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        buttonIsPressed = false;
        menuController = GameObject.Find("MenuController");
        SceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        dataStorage = FindObjectOfType<DataStorage>();
        dataStorage.LoadLastLevelData();
    }

    public void MainMenu()
    {
        StartCoroutine(LoadAsynchronously(0));
    }

    public void StartLevelOne()
    {
        StartCoroutine(LoadAsynchronously(1));
    }

    public void StartLevelTwo()
    {
        StartCoroutine(LoadAsynchronously(2));
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

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        Debug.Log("starting load");

        loadingScreen.SetActive(true);
        Debug.Log("set loading screen active");

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            Debug.Log("sliderprogress: " + progress);
        }
        yield return null;
    }
}
