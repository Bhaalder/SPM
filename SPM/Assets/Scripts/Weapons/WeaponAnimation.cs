using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour{

    private float shakeValue;
    private float shakeDuration;
    private float shakePercentage;
    private float startValue;
    private float startDuration;

    private bool isShaking = false;

    private float recoilValue;
    private float recoilDuration;
    private float recoilPercentage;
    private float startRecoilValue;
    private float startRecoilDuration;

    private bool isRecoiling = false;

    [Header("Smooth Recoil")]
    [SerializeField] private bool isSmooth;
    [SerializeField] private float smoothValue = 3f;

    [Header("Weapons")]
    public GameObject Rifle, Shotgun, RocketLauncher;
    private string weaponName;

    private void Awake()
    {
        Rifle = Camera.main.transform.GetChild(0).GetChild(0).gameObject;
        Shotgun = Camera.main.transform.GetChild(0).GetChild(1).gameObject;
        RocketLauncher = Camera.main.transform.GetChild(0).GetChild(2).gameObject;
    }

    public void RaiseWeaponAnimation(BaseWeapon weapon) {
        weaponName = "";
        switch(weaponName){
            case "Rifle":
                break;
            case "Shotgun":
                break;
            case "Rocket Launcher":
                break;
        }

        Debug.Log("Raising "+ weaponName + "!");
    }

    public void LowerWeaponAnimation(BaseWeapon weapon) {
        weaponName = weapon.GetName();
        switch (weaponName) {
            case "Rifle":
                break;
            case "Shotgun":
                break;
            case "Rocket Launcher":
                break;
        }

        Debug.Log("Lowering " + weaponName + "!");
    }

    public void ReloadWeaponAnimation(BaseWeapon weapon) {
        weaponName = weapon.GetName();
        switch (weaponName) {
            case "Rifle":
                break;
            case "Shotgun":
                break;
            case "Rocket Launcher":
                break;
        }

        Debug.Log("Reloading " + weaponName + "!");
    }

    public void ShootWeaponAnimation(BaseWeapon weapon) {
        weaponName = weapon.GetName();
        switch (weaponName) {
            case "Rifle":
                if (GameController.Instance.GameIsSlowmotion) {
                    RecoilShake(0.4f, 0.1f, Rifle);
                } else {
                    RecoilShake(1f, 0.15f, Rifle);
                }           
                break;
            case "Shotgun":
                if (GameController.Instance.GameIsSlowmotion) {
                    RecoilShake(2.2f, 0.15f, Shotgun);
                } else {
                    RecoilShake(5f, 0.3f, Shotgun);
                }           
                break;
            case "Rocket Launcher":
                if (GameController.Instance.GameIsSlowmotion) {
                    RecoilShake(3.4f, 0.3f, RocketLauncher);
                } else {
                    RecoilShake(7f, 0.7f, RocketLauncher);
                }         
                break;
            default:
                Debug.LogWarning("ShootWeaponAnimation, weaponName not found");
                break;
        }

        Debug.Log("Shooting " + weaponName + "!");
    }
    public void RecoilShake(float value, float duration, GameObject weapon) {
        recoilValue += value;
        startRecoilValue = recoilValue;
        recoilDuration = duration;
        startRecoilDuration = recoilDuration;

        if (!isRecoiling) {
            StartCoroutine(Recoil(weapon));
        }
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
}
