using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour{
    //Author: Patrik Ahlgren

    [Header("Weapon Switch")]
    [SerializeField] private float switchWeaponTime = 0.2f;

    [Header("Weapon Sway")]
    [SerializeField] private float swayAmount = 0.01f;
    [SerializeField] private float maxSwayAmount = 0.05f;
    [SerializeField] private float smoothAmount = 10f;

    [Header("Weapons")]
    public GameObject Rifle, Shotgun, RocketLauncher;
    [SerializeField] private Transform rifleOutOfScreen, shotgunOutOfScreen, rocketLauncherOutOfScreen;
    [SerializeField] private Transform rifleInScreen, shotgunInScreen, rocketLauncherInScreen;
    [SerializeField] private ParticleSystem rifleFlash, shotgunFlash, rocketFlash;
    private Transform weaponCamera;
    private GameObject selectedWeapon;

    private float recoilValue;
    private float recoilDuration;
    private float recoilPercentage;
    private float startRecoilValue;
    private float startRecoilDuration;

    private bool isRecoiling = false;

   

    private void Awake() {
        weaponCamera = Camera.main.transform.GetChild(0);

        Rifle = weaponCamera.GetChild(0).gameObject;
        Shotgun = weaponCamera.GetChild(1).gameObject;
        RocketLauncher = weaponCamera.GetChild(2).gameObject;

        rifleOutOfScreen = weaponCamera.GetChild(3);
        shotgunOutOfScreen = weaponCamera.GetChild(4);
        rocketLauncherOutOfScreen = weaponCamera.GetChild(5);

        rifleInScreen = weaponCamera.GetChild(6);
        shotgunInScreen = weaponCamera.GetChild(7);
        rocketLauncherInScreen = weaponCamera.GetChild(8);

        rifleFlash = Rifle.transform.GetChild(0).GetComponent<ParticleSystem>();
        shotgunFlash = Shotgun.transform.GetChild(0).GetComponent<ParticleSystem>();
        rocketFlash = RocketLauncher.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();

        Rifle.transform.localPosition = rifleOutOfScreen.localPosition;
        Shotgun.transform.localPosition = shotgunOutOfScreen.localPosition;
        RocketLauncher.transform.localPosition = rocketLauncherOutOfScreen.localPosition;

        Rifle.transform.localRotation = rifleOutOfScreen.localRotation;
        Shotgun.transform.localRotation = shotgunOutOfScreen.localRotation;
        RocketLauncher.transform.localRotation = rocketLauncherOutOfScreen.localRotation;
    }

    private void Start() {
        foreach(Transform weapon in weaponCamera) {
            if(weapon.name == GameController.Instance.SelectedWeapon.GetName()) {
                selectedWeapon = weapon.gameObject;
            }
        }
        RaiseWeaponAnimation(selectedWeapon.name);

    }

    private void Update() {
        if (!GameController.Instance.GameIsPaused) {
            float swayX = Input.GetAxis("Mouse X") * swayAmount;
            float swayY = Input.GetAxis("Mouse Y") * swayAmount;

            swayX = Mathf.Clamp(swayX, -maxSwayAmount, maxSwayAmount);
            swayY = Mathf.Clamp(swayY, -maxSwayAmount, maxSwayAmount);

            Vector3 finalPosition = new Vector3(swayX, swayY, 0);
            selectedWeapon.transform.localPosition = Vector3.Lerp(selectedWeapon.transform.localPosition, finalPosition + InitialPositionOfWeapon(), Time.deltaTime * smoothAmount);
        }      
    }

    private Vector3 InitialPositionOfWeapon() {
        switch (selectedWeapon.name) {
            case "Rifle":
                return rifleInScreen.transform.localPosition;
            case "Shotgun":
                return shotgunInScreen.transform.localPosition;
            case "Rocket Launcher":
                return rocketLauncherInScreen.transform.localPosition;
            default:
                Debug.LogWarning("InitialPositionOfWeapon, weaponName not found");
                break;
        }
        return Vector3.zero;
    }

    public void RaiseWeaponAnimation(string weapon) {
        string weaponName = weapon;
        switch (weaponName){
            case "Rifle":
                selectedWeapon = Rifle;
                WeaponPosition(Rifle, switchWeaponTime, rifleOutOfScreen, rifleInScreen);         
                break;
            case "Shotgun":
                selectedWeapon = Shotgun;
                WeaponPosition(Shotgun, switchWeaponTime, shotgunOutOfScreen, shotgunInScreen);             
                break;
            case "Rocket Launcher":
                selectedWeapon = RocketLauncher;
                WeaponPosition(RocketLauncher, switchWeaponTime, rocketLauncherOutOfScreen, rocketLauncherInScreen);               
                break;
            default:
                Debug.LogWarning("RaiseWeaponAnimation, weaponName not found");
                break;
        }

        //Debug.Log("Raising "+ weaponName + "!");
    }

    public void LowerWeaponAnimation(string weapon) {
        string weaponName = weapon;
        switch (weaponName) {
            case "Rifle":
                WeaponPosition(Rifle, switchWeaponTime, Rifle.transform, rifleOutOfScreen);
                break;
            case "Shotgun":
                WeaponPosition(Shotgun, switchWeaponTime, Shotgun.transform, shotgunOutOfScreen);
                break;
            case "Rocket Launcher":
                WeaponPosition(RocketLauncher, switchWeaponTime, RocketLauncher.transform, rocketLauncherOutOfScreen);
                break;
            default:
                Debug.LogWarning("LowerWeaponAnimation, weaponName not found");
                break;
        }

        //Debug.Log("Lowering " + weaponName + "!");
    }

    public void ReloadWeaponAnimation(string weapon) {
        string weaponName = weapon;
        switch (weaponName) {
            case "Rifle":
                break;
            case "Shotgun":
                break;
            case "Rocket Launcher":
                break;
            default:
                Debug.LogWarning("ReloadWeaponAnimation, weaponName not found");
                break;
        }

        //Debug.Log("Reloading " + weaponName + "!");
    }

    public void ShootWeaponAnimation(string weapon) {
        string weaponName = weapon;
        switch (weaponName) {
            case "Rifle":
                if (GameController.Instance.GameIsSlowmotion) {
                    RecoilShake(0.4f, 0.1f, Rifle);
                } else {
                    RecoilShake(1f, 0.15f, Rifle);
                }
                rifleFlash.Play();
                break;
            case "Shotgun":
                if (GameController.Instance.GameIsSlowmotion) {
                    RecoilShake(2.2f, 0.15f, Shotgun);
                } else {
                    RecoilShake(5f, 0.3f, Shotgun);
                }
                shotgunFlash.Play();
                break;
            case "Rocket Launcher":
                if (GameController.Instance.GameIsSlowmotion) {
                    RecoilShake(3.4f, 0.3f, RocketLauncher);
                } else {
                    RecoilShake(7f, 0.7f, RocketLauncher);
                }
                rocketFlash.Play();
                break;
            default:
                Debug.LogWarning("ShootWeaponAnimation, weaponName not found");
                break;
        }

        //Debug.Log("Shooting " + weaponName + "!");
    }

    private void RecoilShake(float value, float duration, GameObject weapon) {
        recoilValue += value;
        startRecoilValue = recoilValue;
        recoilDuration = duration;
        startRecoilDuration = recoilDuration;

        if (!isRecoiling) {
            StartCoroutine(Recoil(weapon));
        }
    }

    private void WeaponPosition(GameObject weapon, float moveDuration, Transform startPos, Transform endPos) {
        StartCoroutine(MoveWeapon(weapon, moveDuration, startPos, endPos));
    }

    private IEnumerator Recoil(GameObject weapon) {
        isRecoiling = true;
        Vector3 rotationAmount;

        while (recoilDuration > 0.01f) {
            if (weapon == RocketLauncher) {
                rotationAmount = new Vector3(-1, 0, 0) * recoilValue;
            } else {
                rotationAmount = new Vector3(-1, 0, -1) * recoilValue;
            }

            recoilPercentage = recoilDuration / startRecoilDuration;
            recoilValue = startRecoilValue * recoilPercentage;
            recoilDuration -= 1 * Time.deltaTime;

            weapon.transform.localRotation = Quaternion.Euler(rotationAmount);

            yield return null;
        }
        weapon.transform.localRotation = Quaternion.identity;

        isRecoiling = false;
    }

    private IEnumerator MoveWeapon(GameObject weapon, float moveDuration, Transform startPos, Transform endPos) {
        Quaternion startRot = startPos.transform.localRotation;
        Quaternion endRot = endPos.transform.localRotation;

        for (float time = 0f; time < moveDuration; time += Time.unscaledDeltaTime) {
            float normalizedTime = time / moveDuration;        
            weapon.transform.localRotation = Quaternion.Lerp(startRot, endRot, normalizedTime);
            weapon.transform.localPosition = Vector3.Lerp(startPos.localPosition, endPos.localPosition, normalizedTime);
            yield return null;
        }

        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localPosition = endPos.localPosition;
    }


}
