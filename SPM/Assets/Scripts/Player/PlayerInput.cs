using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//TA BORT SEN

public class PlayerInput : MonoBehaviour
{
    private PlayerShoot playerShoot;
    public Slowmotion slowmotion;
    private BaseWeapon selectedWeapon;
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
        if (Input.GetKeyDown(KeyCode.T)) {//TA BORT SEN
            SceneManager.LoadScene("Level2WhiteBox");
        }//TA BORT SEN
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
        try {
            BaseWeapon firstWeapon = GameController.Instance.playerWeapons[0];
            BaseWeapon secondWeapon = GameController.Instance.playerWeapons[1];
            BaseWeapon thirdWeapon = GameController.Instance.playerWeapons[2];
            if (Input.GetButtonDown("Weapon1")) {
                if (selectedWeapon == firstWeapon) { } else { GameController.Instance.selectedWeapon = firstWeapon; }
            }
            if (Input.GetButtonDown("Weapon2")) {
                if (selectedWeapon == secondWeapon) { } else { GameController.Instance.selectedWeapon = secondWeapon; }
            }
            if (Input.GetButtonDown("Weapon3")) {
                if (selectedWeapon == thirdWeapon) { } else { GameController.Instance.selectedWeapon = thirdWeapon; }
            }
            
        } catch (System.ArgumentOutOfRangeException) {
            try {
                BaseWeapon firstWeapon = GameController.Instance.playerWeapons[0];
                BaseWeapon secondWeapon = GameController.Instance.playerWeapons[1];
                if (Input.GetButtonDown("Weapon1")) {
                    if (selectedWeapon == firstWeapon) { } else { GameController.Instance.selectedWeapon = firstWeapon; }
                }
                if (Input.GetButtonDown("Weapon2")) {
                    if (selectedWeapon == secondWeapon) { } else { GameController.Instance.selectedWeapon = secondWeapon; }
                }
            } catch (System.ArgumentOutOfRangeException) {
                try {
                    BaseWeapon firstWeapon = GameController.Instance.playerWeapons[0];
                    if (Input.GetButtonDown("Weapon1")) {
                        if (selectedWeapon == firstWeapon) { } else { GameController.Instance.selectedWeapon = firstWeapon; }
                    }
                } catch (System.ArgumentOutOfRangeException) {

                }
            }            
        }
        GameController.Instance.UpdateSelectedWeaponText();
    }
    
    private void SlowmotionInput() {
        if (Input.GetButtonDown("Slowmotion")) {
            slowmotion.SlowTime();
        }
    }
}
