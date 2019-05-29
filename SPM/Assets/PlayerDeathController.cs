using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{

    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject respawnManager;
    [SerializeField] private GameObject player;

    private Vector3 deathLocation;


    // Start is called before the first frame update
    void Start()
    {
        if (GameController.Instance.PlayerHP == 0)
        {
            GameController.Instance.PlayerHP = 10;
            GameController.Instance.PlayerArmor = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.PlayerHP <= 0)
        {
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
        deathPanel.SetActive(false);
    }

    public void RespawnAtDeathLocation()
    {
        resetStatus();
        DisableCursor();
        PauseAndUnpauseGame();
        deathPanel.SetActive(false);
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
    }

    private void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


}
