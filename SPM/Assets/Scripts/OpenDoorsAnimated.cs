using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsAnimated : MonoBehaviour
{
    //Main Author: Teo
    //Secondary Author: Patrik Ahlgren
    bool isOpen;
    bool isClosed;

    public GameObject redPanel;
    public GameObject greenPanel;
    public SpawnManager spawnManager;

    public Animator anim;

    public bool spawnEnemies;

    void Start()
    {
        greenPanel = gameObject.transform.GetChild(0).gameObject;
        redPanel = gameObject.transform.GetChild(1).gameObject;
        greenPanel.SetActive(false);
        redPanel.SetActive(true);  
    }

    public void OpenAndClose() {
        isOpen = !isOpen;
        Debug.Log("F");
        if (isOpen) {
            anim.SetBool("isOpen", true);
            greenPanel.SetActive(!greenPanel.activeSelf);
            redPanel.SetActive(!redPanel.activeSelf);
            if (spawnEnemies) {
                spawnManager.InitializeSpawner();
                spawnEnemies = false;
            }

        } else {
            anim.SetBool("isOpen", false);
            greenPanel.SetActive(!greenPanel.activeSelf);
            redPanel.SetActive(!redPanel.activeSelf);
        }
    }
}

