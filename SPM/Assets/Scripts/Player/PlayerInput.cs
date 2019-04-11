using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerShoot playerShoot;
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
        Weapon first = GameController.Instance.playerWeapons[0];
        Weapon second = GameController.Instance.playerWeapons[1];
        Weapon third = GameController.Instance.playerWeapons[2];
        if (Input.GetButtonDown("Weapon1")) {
            if(selectedWeapon == first) {}
            else {GameController.Instance.selectedWeapon = first;}
        }
        if (Input.GetButtonDown("Weapon2")) {
            if (selectedWeapon == second) {}
            else {GameController.Instance.selectedWeapon = second;}
        }
        if (Input.GetButtonDown("Weapon3")) {
            if (selectedWeapon == third) {} 
            else {GameController.Instance.selectedWeapon = third;}
        }
        GameController.Instance.UpdateSelectedWeaponText();
    }
        
}
