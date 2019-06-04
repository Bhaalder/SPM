using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    //Author: Marcus Söderberg
    public GameObject menuPanel;
    public GameObject EndGamePanel;

    private GameObject scenemanager;

    public bool InGameMenuActive;


    // Start is called before the first frame update
    void Start()
    {
        menuPanel.SetActive(false);
        InGameMenuActive = false;
        //DontDestroyOnLoad(gameObject);
        scenemanager = GameObject.Find("SceneManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMenu()
    {
        menuPanel.SetActive(true);
        InGameMenuActive = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameController.Instance.PauseAudio = true;
        GameController.Instance.GamePaused();

    }

    public void DeactivateMenu()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuPanel.SetActive(false);
        InGameMenuActive = false;
        GameController.Instance.GamePaused();
    }

    public void MainMenu()
    {
        Debug.Log("Clicked button: Main Menu");
        Destroy(GameObject.Find("Canvas"));
        Destroy(GameObject.Find("GameController"));
        AudioController.Instance.StopAllSounds();
        Destroy(GameObject.Find("AudioController"));
        scenemanager.GetComponent<SceneManagerScript>().MainMenu();
        DeactivateMenu();
    }

    public void Restart()
    {
        scenemanager.GetComponent<SceneManagerScript>().StartLevelOne();
    }

    public void EndGameActivate()
    {

        //End button activates

        scenemanager = GameObject.Find("SceneManager");
        EndGamePanel.SetActive(true);
        InGameMenuActive = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameController.Instance.PauseAudio = true;
        GameController.Instance.GamePaused();
    }

    public void EndGameDeactivate()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        EndGamePanel.SetActive(false);
        InGameMenuActive = false;
        GameController.Instance.GamePaused();
    }

    public void SaveGame()
    {
        GameController.Instance.SaveGame();
    }

    public void LoadGame()
    {
        GameController.Instance.LoadGame();
        DeactivateMenu();
    }

}
