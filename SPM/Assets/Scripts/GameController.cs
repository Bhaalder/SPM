﻿using System.Collections;
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
        DontDestroyOnLoad(gameObject);
        HealthSlider.value = playerHP;
        ArmorSlider.value = playerArmor;

        //detta ger spelaren alla vapen direkt
        Weapon rifle = new Weapon("Rifle", 10, 100, 10f, 0.5f, 1, 50, 50, 100, null, null, null);
        playerWeapons.Add(rifle);
        Weapon shotgun = new Weapon("Shotgun", 30, 100, 2f, 0.5f, 1, 25, 25, 100, null, null, null);
        playerWeapons.Add(shotgun);
        Weapon rocketLauncher = new ProjectileWeapon("Rocket Launcher", 50, 100, 1f, 0.3f, 1, 1, 5, 5, 100, null, null, null);
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
        if (playerArmor <= 0) { playerHP -= damage; }
        else { playerArmor -= damage; }

    }

}
