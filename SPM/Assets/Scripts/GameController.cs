using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public List<MonoBehaviour> subscribedScripts = new List<MonoBehaviour>();
    public List<Weapon> playerWeapons = new List<Weapon>();
    public int gameEventID; //detta är till för att markera vissa händelser i spelet

    public GameObject player;

    public Weapon selectedWeapon;

    public Slider HealthSlider, ArmorSlider;
    public Text weaponNameText, weaponAmmoText;

    public int playerHP, playerArmor;

    //detta ifall man vill kunna byta vapen senare men fortfarande ha en begränsning av 3 vapen men ha kvar info om hur mkt ammo man har kvar
    public int totalRifleAmmunition, totalShotgunAmmunition, totalRocketLauncherAmmunition;

    public bool gameIsPaused;
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

        //detta ger spelaren alla vapen direkt
        Weapon rifle = new Weapon("Rifle", 10, 100, 10f, 0.5f, 0.1f, 15, 50, 50, 999, null, null, null);
        playerWeapons.Add(rifle);
        //TESTAR SHOTGUN
        Weapon shotgun = new Weapon("Shotgun", 30, 18, 2f, 0.5f, 0.1f, 30, 25, 25, 100, null, null, null);
        playerWeapons.Add(shotgun);
        Weapon rocketLauncher = new ProjectileWeapon("Rocket Launcher", 30, 100, 1f, 0.3f, 0.01f, 20, 10, 5, 5, 100, null, null, null, null);
        playerWeapons.Add(rocketLauncher);

        totalRifleAmmunition = rifle.GetTotalAmmoLeft();
        totalShotgunAmmunition = shotgun.GetTotalAmmoLeft();
        totalRocketLauncherAmmunition = rocketLauncher.GetTotalAmmoLeft();

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
        if (playerArmor <= 0) { playerHP -= damage; Debug.Log("damage has arrived"); }
        else { playerArmor -= damage; }

    }

}
