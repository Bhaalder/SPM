using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//TA BORT SEN

public class PlayerInput : MonoBehaviour
{

    public float dashForce;
    public float nextTimeToDash;
    public Slowmotion slowmotion;

    private PlayerShoot playerShoot;
    private BaseWeapon selectedWeapon;
    private Rigidbody rigidBody;
    private float nextTimeToFireOrReload = 0f;
    private float nextTimeToReload = 0f;
    private bool isReloading = false;

    private void Start(){
        playerShoot = GetComponentInChildren<PlayerShoot>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update(){
        selectedWeapon = GameController.Instance.selectedWeapon;
        ReloadWeaponInput();
        ReloadSequence();
        ShootWeaponInput();
        SwitchWeaponInput();
        SlowmotionInput();
        InteractInput();
        DashInput();


        if (Input.GetKeyDown(KeyCode.T)) {//TA BORT SEN
            SceneManager.LoadScene("Level2WhiteBox");
        }//TA BORT SEN


    }


    private void ReloadWeaponInput() {      
        if (Input.GetButtonDown("Reload") && Time.time >= nextTimeToReload) {
            ReloadWeapon();
        }
    }

    private void ReloadWeapon() {
        int ammoInClip = selectedWeapon.GetAmmoInClip();
        int maxAmmoInClip = selectedWeapon.GetMaxAmmoInClip();
        int totalAmmoLeft = selectedWeapon.GetTotalAmmoLeft();

        if (ammoInClip != maxAmmoInClip && totalAmmoLeft > 0) {
            nextTimeToFireOrReload = Time.time + 1f / selectedWeapon.GetReloadTime();
            nextTimeToReload = Time.time + 1f / selectedWeapon.GetReloadTime();
            int ammoSpent = maxAmmoInClip - ammoInClip;
            if (ammoSpent > totalAmmoLeft) {

                selectedWeapon.SetAmmoInClip(ammoInClip + totalAmmoLeft);
                selectedWeapon.SetTotalAmmoLeft(0);
                Debug.Log("Reloading " + selectedWeapon.GetName());
                GameController.Instance.ReloadSlider.gameObject.SetActive(true);
                isReloading = true;
                return;
            }
            selectedWeapon.SetAmmoInClip(maxAmmoInClip);
            selectedWeapon.SetTotalAmmoLeft(totalAmmoLeft - ammoSpent);
            Debug.Log("Reloading " + selectedWeapon.GetName());
            GameController.Instance.ReloadSlider.gameObject.SetActive(true);
            isReloading = true;
        }
    }

    private void ReloadSequence() {     
        if (isReloading) {
            GameController.Instance.ReloadSlider.maxValue = 1f / selectedWeapon.GetReloadTime();
            GameController.Instance.ReloadSlider.value += 1f * Time.deltaTime;
            if (Time.time >= nextTimeToReload) {
                isReloading = false;
                GameController.Instance.ReloadSlider.value = 0;
                GameController.Instance.UpdateSelectedWeaponAmmoText();
                GameController.Instance.ReloadSlider.gameObject.SetActive(false);
            }
        }
    }

    private void ShootWeaponInput() {
        if (Input.GetButton("Fire1") && GameController.Instance.selectedWeapon.GetAmmoInClip() == 0) {
            ReloadWeapon();
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFireOrReload) {
            nextTimeToFireOrReload = Time.time + 1f/selectedWeapon.GetFireRate();
            playerShoot.StartShooting(selectedWeapon);
        }
    }

    private void SwitchWeaponInput() {
       if (!isReloading) {
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
        }//if !isReloading
    }
    
    private void SlowmotionInput() {
        if (Input.GetButtonDown("Slowmotion")) {
            slowmotion.SlowTime();
        }
    }

    private void InteractInput() {
        if (Input.GetButtonDown("Interact")) {
            GameController.Instance.playerIsInteracting = true;
            Debug.Log("Player tried to interact");
        } else {GameController.Instance.playerIsInteracting = false;}
    }

    private void DashInput() {
        if (Input.GetButtonDown("Dash")) {

            //rigidBody.AddForce(transform.forward * dashForce, ForceMode.Impulse);

            //playerShoot.Melee();
            Debug.Log("Dash");
        }
    }



}
