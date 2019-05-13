using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//TA BORT SEN

public class PlayerInput : MonoBehaviour {
    //Author: Patrik Ahlgren
    public Slowmotion slowmotion;

    public GameObject startTeleport;
    public GameObject secondTeleport;
    public GameObject thirdTeleport;

    private PlayerShoot playerShoot;
    private PlayerMovementController playerMovementController;
    private BaseWeapon selectedWeapon;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    private void Start() {
        playerShoot = GetComponentInChildren<PlayerShoot>();
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    private void Update() {
        selectedWeapon = GameController.Instance.selectedWeapon;
        ReloadWeaponInput();
        ReloadSequence();
        ShootWeaponInput();
        SwitchWeaponInput();
        SlowmotionInput();
        InteractInput();
        DashInput();
        InGameMenu();

        Teleport();//TA BORT SEN

    }

    private void Teleport() {//TA BORT SEN
        if (Input.GetKeyDown(KeyCode.T)) {
            SceneManager.LoadScene("Level2WhiteBox");
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            try {
                transform.position = startTeleport.transform.position;
            } catch (System.Exception) {
                Debug.Log("FINNS INGEN DEFINERAD 'startTeleport'");
            }
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            try {
                transform.position = secondTeleport.transform.position;
            } catch (System.Exception) {
                Debug.Log("FINNS INGEN DEFINERAD 'secondTeleport'");
            }
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            try {
                transform.position = thirdTeleport.transform.position;
            } catch (System.Exception) {
                Debug.Log("FINNS INGEN DEFINERAD 'thirdTeleport'");
            }
        }
    }//TA BORT SEN

    private void InGameMenu() {
        if (Input.GetKeyDown(KeyCode.F10)) {
            try {
                GameObject menucontroller = GameObject.Find("MenuController");
                if (menucontroller.GetComponent<MenuController>().InGameMenuActive) {
                    menucontroller.GetComponent<MenuController>().DeactivateMenu();
                } else {
                    menucontroller.GetComponent<MenuController>().ActivateMenu();
                }
            } catch (System.Exception) {
                Debug.Log("FINNS INGEN DEFINERAD 'MenuController'");
            }
        }
    }


    private void ReloadWeaponInput() {
        if (Input.GetButtonDown("Reload")) {
            ReloadWeapon();
        }
    }

    private void ReloadWeapon() {
        int ammoInClip = selectedWeapon.GetAmmoInClip();
        int maxAmmoInClip = selectedWeapon.GetMaxAmmoInClip();
        int totalAmmoLeft = selectedWeapon.GetTotalAmmoLeft();

        if (ammoInClip != maxAmmoInClip && totalAmmoLeft > 0 && !isReloading) {
            int ammoSpent = maxAmmoInClip - ammoInClip;
            GameController.Instance.ReloadSlider.gameObject.SetActive(true);
            Debug.Log("Reloading " + selectedWeapon.GetName());
            GameController.Instance.ReloadSlider.maxValue = selectedWeapon.GetReloadTime();
            isReloading = true;
        }
    }

    private void ReloadSequence() {
        if (isReloading) {
            GameController.Instance.ReloadSlider.value += 1 * Time.fixedUnscaledDeltaTime;
        }
        if (GameController.Instance.ReloadSlider.value >= selectedWeapon.GetReloadTime()) {
            GameController.Instance.ReloadSlider.value = 0;
            FinishReload(selectedWeapon.GetAmmoInClip(), selectedWeapon.GetTotalAmmoLeft(), selectedWeapon.GetMaxAmmoInClip() - selectedWeapon.GetAmmoInClip());
            GameController.Instance.UpdateSelectedWeaponAmmoText();
            GameController.Instance.ReloadSlider.gameObject.SetActive(false);
            Debug.Log(GameController.Instance.ReloadSlider.maxValue);
            isReloading = false;
        }
    }

    private void FinishReload(int ammoInClip, int totalAmmoLeft, int ammoSpent) {
        if (ammoSpent > totalAmmoLeft) {
            selectedWeapon.SetAmmoInClip(ammoInClip + totalAmmoLeft);
            selectedWeapon.SetTotalAmmoLeft(0);
            return;
        }
        selectedWeapon.SetAmmoInClip(selectedWeapon.GetMaxAmmoInClip());
        selectedWeapon.SetTotalAmmoLeft(totalAmmoLeft - ammoSpent);
        StopAllCoroutines();
    }

    private void AbortReload() {
        isReloading = false;
        GameController.Instance.ReloadSlider.value = 0;
        GameController.Instance.ReloadSlider.gameObject.SetActive(false);
    }

    private void ShootWeaponInput() {
        if (!isReloading) {
            if (Input.GetButton("Fire1") && GameController.Instance.selectedWeapon.GetAmmoInClip() == 0) {
                ReloadWeapon();
            }
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) {
                nextTimeToFire = Time.time + 1f / selectedWeapon.GetFireRate();
                playerShoot.StartShooting(selectedWeapon);
            }
        }
    }

    private void SwitchWeaponInput() {
        BaseWeapon firstWeapon = null;
        BaseWeapon secondWeapon = null;
        BaseWeapon thirdWeapon = null;
        try {
            GetWeaponFromGameController(ref firstWeapon, 0);
            GetWeaponFromGameController(ref secondWeapon, 1);
            GetWeaponFromGameController(ref thirdWeapon, 2);
        } catch (System.ArgumentOutOfRangeException) {

        }
        if (Input.GetButtonDown("Weapon1") && firstWeapon != null) {
            AbortReload();
            if (selectedWeapon != firstWeapon) { GameController.Instance.selectedWeapon = firstWeapon; }
        }
        if (Input.GetButtonDown("Weapon2") && secondWeapon != null) {
            AbortReload();
            if (selectedWeapon != secondWeapon) { GameController.Instance.selectedWeapon = secondWeapon; }
        }
        if (Input.GetButtonDown("Weapon3") && thirdWeapon != null) {
            AbortReload();
            if (selectedWeapon != thirdWeapon) { GameController.Instance.selectedWeapon = thirdWeapon; }
        }

        GameController.Instance.UpdateSelectedWeaponText();
    }

    private BaseWeapon GetWeaponFromGameController(ref BaseWeapon weapon, int i) {
        if (GameController.Instance.playerWeapons[i] != null) {
            return weapon = GameController.Instance.playerWeapons[i];
        } else {
            return null;
        }
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
        } else { GameController.Instance.playerIsInteracting = false; }
    }

    private void DashInput() {
        if (Input.GetButtonDown("Dash")) {
            playerMovementController.Dash();
            //playerShoot.Melee();
            Debug.Log("Dash");
        }
    }



}
