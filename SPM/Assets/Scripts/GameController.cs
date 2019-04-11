using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public List<MonoBehaviour> subscribedScripts = new List<MonoBehaviour>();
    public int gameEventID; //detta är till för att markera vissa händelser i spelet

    public GameObject player;
    public int playerHP, playerArmor, playerAmmunition;
    public Slider HealthSlider, ArmorSlider;
    public bool gameIsPaused;

 
    private static GameController instance;

    public static GameController Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GameController>();
#if UNITY_EDITOR
                if (FindObjectsOfType<GameController>().Length > 1) {
                    Debug.LogError("Found more than one gamecontroller");
                }
#endif
            }
            return instance;
        }
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        HealthSlider.value = playerHP;
        ArmorSlider.value = playerArmor;
    }

    public void PlayerPassedEvent() {
        gameEventID++;
    }

    public void GamePaused() {
        if (gameIsPaused) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }

    void Update() {
        HealthSlider.value = playerHP;
        ArmorSlider.value = playerArmor;
    }

    public void TakeDamage(int damage) {
        if (playerArmor <= 0) { playerHP -= damage; }
        else { playerArmor -= damage; }

    }

}
