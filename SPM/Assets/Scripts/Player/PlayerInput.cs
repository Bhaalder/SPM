using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//TA BORT SEN

public class PlayerInput : MonoBehaviour {
    //Author: Patrik Ahlgren
    [SerializeField] private Slowmotion slowmotion;
    [SerializeField] private Transform weaponCamera;

    [SerializeField] private GameObject startTeleport;//TA BORT SEN
    [SerializeField] private GameObject secondTeleport;//TA BORT SEN
    [SerializeField] private GameObject thirdTeleport;//TA BORT SEN

    private PlayerShoot playerShoot;
    private PlayerMovementController playerMovementController;
    private BaseWeapon selectedWeapon;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    private bool skipShootDelayToSlowmotion;

    private void Start() {
        playerShoot = GetComponentInChildren<PlayerShoot>();
        playerMovementController = GetComponent<PlayerMovementController>();
        weaponCamera = Camera.main.transform.GetChild(0);
        slowmotion = GameController.Instance.GetComponent<Slowmotion>();
        GameController.Instance.Player = gameObject;
        ActivateSelectedWeaponGameObject(GameController.Instance.SelectedWeapon);
    }


    private void Update() {
        selectedWeapon = GameController.Instance.SelectedWeapon;
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

    #region Reload Methods
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
            PlayReloadSound();
            int ammoSpent = maxAmmoInClip - ammoInClip;
            GameController.Instance.ReloadSlider.gameObject.SetActive(true);
            GameController.Instance.ReloadSlider.maxValue = selectedWeapon.GetReloadTime();
            isReloading = true;
        }
    }

    private void ReloadSequence() {
        if (isReloading) {
            if (GameController.Instance.GameIsPaused) {

            } else {
                GameController.Instance.ReloadSlider.value += 1 * Time.unscaledDeltaTime;
                
            }          
        }
        if (GameController.Instance.ReloadSlider.value >= selectedWeapon.GetReloadTime()) {
            int ammoInClip = selectedWeapon.GetAmmoInClip();
            int maxAmmoInClip = selectedWeapon.GetMaxAmmoInClip();
            int totalAmmoLeft = selectedWeapon.GetTotalAmmoLeft();
            int ammoSpent = maxAmmoInClip - ammoInClip;
            GameController.Instance.ReloadSlider.value = 0;
            FinishReload(ammoInClip, totalAmmoLeft, ammoSpent);
            GameController.Instance.UpdateSelectedWeapon_AmmoText();
            GameController.Instance.ReloadSlider.gameObject.SetActive(false);
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

    public void AbortReload() {
        isReloading = false;
        GameController.Instance.ReloadSlider.value = 0;
        GameController.Instance.ReloadSlider.gameObject.SetActive(false);
        StopReloadSound();
    }

    private void PlayReloadSound() {
        string name = selectedWeapon.GetName();
        if(name == "Rifle") {
            AudioController.Instance.PlaySFX_RandomPitch("Rifle_Reload", 0.95f, 1f);
        }
        if(name == "Shotgun") {
            AudioController.Instance.PlaySFX_RandomPitch("Shotgun_Reload", 0.95f, 1f);
        }
        if (name == "Rocket Launcher") {
            AudioController.Instance.PlaySFX_RandomPitch("RocketLauncher_Reload", 0.95f, 1f);
        }
    }
    private void StopReloadSound() {
        AudioController.Instance.Stop("Rifle_Reload");
        AudioController.Instance.Stop("Shotgun_Reload");
        AudioController.Instance.Stop("RocketLauncher_Reload");
    }

    #endregion

    #region Shoot Method
    private void ShootWeaponInput() {
        if (!isReloading) {
            if (Input.GetButton("Fire1") && GameController.Instance.SelectedWeapon.GetAmmoInClip() == 0) {
                ReloadWeapon();
                return;
            }
            if (!GameController.Instance.GameIsSlowmotion) {
                skipShootDelayToSlowmotion = true;
            }
            if (GameController.Instance.GameIsSlowmotion && Time.time <= nextTimeToFire && skipShootDelayToSlowmotion) {
                nextTimeToFire = Time.time + 1f / selectedWeapon.GetFireRate();
                skipShootDelayToSlowmotion = false;
            }
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) {
                nextTimeToFire = Time.time + 1f / selectedWeapon.GetFireRate();
                playerShoot.StartShooting(selectedWeapon);
            }
        }
    }
    #endregion

    #region SwitchWeapon Methods
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
            WeaponController.Instance.GetComponent<WeaponAnimation>().RaiseWeapon();
            if (selectedWeapon != firstWeapon){
                GameController.Instance.SelectedWeapon = firstWeapon;
                ActivateSelectedWeaponGameObject(firstWeapon);
            }
        }
        if (Input.GetButtonDown("Weapon2") && secondWeapon != null) {
            AbortReload();
            if (selectedWeapon != secondWeapon) {
                GameController.Instance.SelectedWeapon = secondWeapon;
                ActivateSelectedWeaponGameObject(secondWeapon);
            }
        }
        if (Input.GetButtonDown("Weapon3") && thirdWeapon != null) {
            AbortReload();
            if (selectedWeapon != thirdWeapon) {
                GameController.Instance.SelectedWeapon = thirdWeapon;
                ActivateSelectedWeaponGameObject(thirdWeapon);
            }
        }
        GameController.Instance.UpdateSelectedWeapon();
    }

    private BaseWeapon GetWeaponFromGameController(ref BaseWeapon weapon, int i) {
        if (GameController.Instance.PlayerWeapons[i] != null) {
            return weapon = GameController.Instance.PlayerWeapons[i];
        } else {
            return null;
        }
    }

    private void ActivateSelectedWeaponGameObject(BaseWeapon selectedWeapon) {
        foreach(Transform weapon in weaponCamera) {
            if(weapon.name == selectedWeapon.GetName()) {
                //weapon.transform.position = new Vector3(0, 0, 0f);
            } else {
                //weapon.transform.position = new Vector3(0, 0, -50f);
            }
        }
    }

    #endregion

    #region Slowmotion Method
    private void SlowmotionInput() {
        if (Input.GetButtonDown("Slowmotion")) {
            slowmotion.SlowTime();
        }
    }
    #endregion

    #region Interaction Method
    private void InteractInput() {
        if (Input.GetButtonDown("Interact")) {
            GameController.Instance.PlayerIsInteracting = true;
            Debug.Log("Player tried to interact");
        } else { GameController.Instance.PlayerIsInteracting = false; }
    }
    #endregion

    #region Dash Method
    private void DashInput() {
        if (Input.GetButtonDown("Dash")) {
            playerMovementController.Dash();
            //playerShoot.Melee();
        }
    }
    #endregion

    #region Menu Method
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
    #endregion

}
