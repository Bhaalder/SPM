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
    private int level1 = 1;
    private int level2 = 2;
    private int mainMenuIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        buttonIsPressed = false;
        menuController = GameObject.Find("MenuController");
        SceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        dataStorage = FindObjectOfType<DataStorage>();
        dataStorage.LoadLastLevelData();
        loadingScreen.SetActive(false);

    }

    public void MainMenu()
    {
        dataStorage.SaveGame();
        Destroy(GameObject.Find("Canvas"));
        Destroy(GameObject.Find("GameController"));
        AudioController.Instance.StopAllSounds();
        Destroy(GameObject.Find("AudioController"));
        SceneManager.LoadScene(mainMenuIndex);
        //StartCoroutine(WaitForSeconds());
    }

    public void StartLevelOne()
    {
        SaveSystem.DeleteAllSaveFiles();
        StartCoroutine(LoadAsynchronously(level1));
    }

    public void StartLevelTwo()
    {
        StartCoroutine(LoadAsynchronously(level2));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinuePreviousGame()
    {
        StartCoroutine(LoadAsynchronously(dataStorage.SceneBuildIndex));
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

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(3f);
        Destroy(GameObject.Find("Canvas"));
        Destroy(GameObject.Find("GameController"));
        AudioController.Instance.StopAllSounds();
        Destroy(GameObject.Find("AudioController"));
        StartCoroutine(LoadAsynchronously(mainMenuIndex));
    }
}
