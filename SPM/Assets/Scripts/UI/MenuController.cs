using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;

    private GameObject scenemanager;


    // Start is called before the first frame update
    void Start()
    {
        menuPanel.SetActive(false);
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
    }

    public void DeactivateMenu()
    {
        menuPanel.SetActive(false);
    }

    public void MainMenu()
    {
        scenemanager.GetComponent<SceneManagerScript>().MainMenu();
    }

}
