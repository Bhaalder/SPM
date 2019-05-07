﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;

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
        scenemanager.GetComponent<SceneManagerScript>().MainMenu();
    }

}