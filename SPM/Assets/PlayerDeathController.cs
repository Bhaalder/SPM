using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{

    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject respawnManager;
    [SerializeField] private GameObject player;

    [SerializeField] private Vector3 deathLocation;
    [SerializeField] private bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        LoadGameObjectReferences();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.PlayerHP <= 0 && isDead == false)
        {
            isDead = true;
            LoadGameObjectReferences();
            OnPlayerDeath(player.transform.position);
        }
    }

    public void OnPlayerDeath(Vector3 deathlocation)
    {
        deathLocation = deathlocation;
        deathPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseAndUnpauseGame();
    }

    public void RespawnAtLastCheckpoint()
    {
        respawnManager.GetComponent<PlayerRespawner>().RespawnMethod();
        DisableCursor();
        PauseAndUnpauseGame();
        isDead = false;
        deathPanel.SetActive(false);
    }

    public void RespawnAtDeathLocation()
    {        
        resetStatus();
        DisableCursor();
        PauseAndUnpauseGame();
        deathPanel.SetActive(false);
        player.transform.position = player.transform.position + player.transform.up *5;
    }

    public void SaveAndExit()
    {
        //Call on save and exit methods
    }

    private void PauseAndUnpauseGame()
    {
        GameController.Instance.GamePaused();
    }
    public void resetStatus()
    {
        GameController.Instance.PlayerHP = 100;
        GameController.Instance.PlayerArmor = 100;
        isDead = false;
    }

    private void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadGameObjectReferences()
    {
        player = GameController.Instance.Player;
        respawnManager = GameObject.FindObjectOfType<PlayerRespawner>().gameObject;
    }


}
