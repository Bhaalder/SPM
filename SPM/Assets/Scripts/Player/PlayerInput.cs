﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerShoot playerShoot;
    public Slowmotion slowmotion;
    private Weapon selectedWeapon;
    private float nextTimeToFireOrReload = 0f;



    private void Start(){
        playerShoot = GetComponentInChildren<PlayerShoot>();
    }

    private void Update(){
        selectedWeapon = GameController.Instance.selectedWeapon;
        ReloadWeaponInput();
        ShootWeaponInput();
        SwitchWeaponInput();
        SlowmotionInput();
    }
    private void ReloadWeaponInput() {      
        int ammoInClip = selectedWeapon.GetAmmoInClip();
        int maxAmmoInClip = selectedWeapon.GetMaxAmmoInClip();
        int totalAmmoLeft = selectedWeapon.GetTotalAmmoLeft();

        if (Input.GetButtonDown("Reload") && Time.time >= nextTimeToFireOrReload) {
            if (ammoInClip != maxAmmoInClip && totalAmmoLeft > 0) {
                nextTimeToFireOrReload = Time.time + 1f / selectedWeapon.GetReloadTime();
                int ammoSpent = maxAmmoInClip - ammoInClip;
                if (ammoSpent > totalAmmoLeft) {

                    selectedWeapon.SetAmmoInClip(ammoInClip + totalAmmoLeft);
                    selectedWeapon.SetTotalAmmoLeft(0);
                    Debug.Log("Reloading " + selectedWeapon.GetName());
                    if (Time.time >= nextTimeToFireOrReload) {
                        GameController.Instance.UpdateSelectedWeaponAmmoText();
                    }
                    return;
                }
                selectedWeapon.SetAmmoInClip(maxAmmoInClip);
                selectedWeapon.SetTotalAmmoLeft(totalAmmoLeft - ammoSpent);
                Debug.Log("Reloading " + selectedWeapon.GetName());
                if (Time.time >= nextTimeToFireOrReload) {
                    GameController.Instance.UpdateSelectedWeaponAmmoText();
                }
            }
        }
    }

    private void ShootWeaponInput() {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFireOrReload) {
            nextTimeToFireOrReload = Time.time + 1f/selectedWeapon.GetFireRate();
            playerShoot.StartShooting(selectedWeapon);
        }
    }

    private void SwitchWeaponInput() {
        Weapon firstWeapon = GameController.Instance.playerWeapons[0];
        Weapon secondWeapon = GameController.Instance.playerWeapons[1];
        Weapon thirdWeapon = GameController.Instance.playerWeapons[2];
        if (Input.GetButtonDown("Weapon1")) {
            if(selectedWeapon == firstWeapon) {}
            else {GameController.Instance.selectedWeapon = firstWeapon;}
        }
        if (Input.GetButtonDown("Weapon2")) {
            if (selectedWeapon == secondWeapon) {}
            else {GameController.Instance.selectedWeapon = secondWeapon;}
        }
        if (Input.GetButtonDown("Weapon3")) {
            if (selectedWeapon == thirdWeapon) {} 
            else {GameController.Instance.selectedWeapon = thirdWeapon;}
        }
        GameController.Instance.UpdateSelectedWeaponText();
    }
    
    private void SlowmotionInput() {
        if (Input.GetButtonDown("Slowmotion")) {
            slowmotion.SlowTime();
        }
    }
}
