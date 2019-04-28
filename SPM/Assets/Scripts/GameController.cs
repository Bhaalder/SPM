using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public List<MonoBehaviour> subscribedScripts = new List<MonoBehaviour>();
    public List<BaseWeapon> playerWeapons = new List<BaseWeapon>();
    public int gameEventID = 1; //detta är till för att markera vissa händelser i spelet
    public bool sceneCompleted;
    public Text winText;

    public GameObject player;

    public BaseWeapon selectedWeapon;

    public Slider HealthSlider, ArmorSlider, SlowmotionSlider, ReloadSlider;
    public Text weaponNameText, weaponAmmoText;

    public int playerHP, playerArmor;
    public float slowMotionTime, reloadTime;

    public bool gameIsPaused, playerIsInteracting;
    public bool gameIsSlowmotion = false;

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

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        DontDestroyOnLoad(gameObject);

        HealthSlider.value = playerHP;
        ArmorSlider.value = playerArmor;
        SlowmotionSlider.value = SlowmotionSlider.maxValue;
        ReloadSlider.value = reloadTime;

        BaseWeapon rifle = WeaponController.Instance.GetRifle();
        playerWeapons.Add(rifle);
        //TESTAR SHOTGUN (shotgun stats finns nu i weaponcontroller)   
        //BaseWeapon shotgun = WeaponController.Instance.GetShotgun();
        //playerWeapons.Add(shotgun);
        //BaseWeapon rocketLaucher = WeaponController.Instance.GetRocketLauncher();
        //playerWeapons.Add(rocketLaucher);

        selectedWeapon = playerWeapons[0];
        UpdateSelectedWeaponText();
        
    }

    public void UpdateSelectedWeaponText() {
        weaponNameText.text = selectedWeapon.GetName();
        UpdateSelectedWeaponAmmoText();
    }

    public void UpdateSelectedWeaponAmmoText() {
        weaponAmmoText.text = "Ammo: " + selectedWeapon.GetAmmoInClip() + "/" + selectedWeapon.GetMaxAmmoInClip() + " (" + selectedWeapon.GetTotalAmmoLeft() + ")";
    }

    public void SceneCompletedSequence() {
        winText.gameObject.SetActive(true);
        sceneCompleted = true;
    }
    public void SceneNotCompletedSequence() {
        winText.gameObject.SetActive(false);
        sceneCompleted = false;
    }

    public void PlayerPassedEvent() {
        gameEventID++;
        Debug.Log("Game event =" + gameEventID);
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


    public void TakeDamage(int damage){
        if (playerArmor <= 0) { playerHP -= damage; Debug.Log("damage has arrived"); }
        else { playerArmor -= damage; }
        HealthSlider.value = playerHP;
        ArmorSlider.value = playerArmor;
    }

}
