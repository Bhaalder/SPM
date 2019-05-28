using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour{

    [Header("Shake")]
    [SerializeField] private float shakeValue;
    [SerializeField] private float shakeDuration;

    private float shakePercentage;
    private float startValue;
    private float startDuration;

    private bool isShaking = false;

    [Header("Recoil")]
    [SerializeField] private float recoilValue;
    [SerializeField] private float recoilDuration;

    private float recoilPercentage;
    private float startRecoilValue;
    private float startRecoilDuration;

    private bool isRecoiling = false;

    [Header("Smooth")]
    [SerializeField] private bool isSmooth;
    [SerializeField] private float smoothValue = 3f;

    [Header("Weapons")]
    [SerializeField] private GameObject rifle, shotgun, rocketLauncher;
    private string weaponName;

    private void Start()
    {
        rifle = Camera.main.transform.GetChild(0).GetChild(0).gameObject;
        shotgun = Camera.main.transform.GetChild(0).GetChild(1).gameObject;
        rocketLauncher = Camera.main.transform.GetChild(0).GetChild(2).GetChild(0).gameObject;
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
        weaponName = "";
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
        weaponName = "";
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
                RecoilShake(1f, 0.15f, rifle);
                break;
            case "Shotgun":
                RecoilShake(5f, 0.3f, shotgun);
                break;
            case "Rocket Launcher":
                RecoilShake(0.1f, 0.1f, rocketLauncher);
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

    private void Shake(float value, float duration, GameObject weapon) {
        ShakeValueAndDuration(value, duration);

        if (!isShaking) {
            StartCoroutine(ShakeWeapon(weapon));
        }
    }

    private void ShakeValueAndDuration(float value, float duration) {
        shakeValue += value;
        startValue = shakeValue;
        shakeDuration = duration;
        startDuration = shakeDuration;
    }

    private IEnumerator Recoil(GameObject weapon) {
        isRecoiling = true;

        while (recoilDuration > 0.01f) {
            Vector3 rotationAmount = new Vector3(-1, 0, -1) * recoilValue;

            recoilPercentage = recoilDuration / startRecoilDuration;

            recoilValue = startRecoilValue * recoilPercentage;
            recoilDuration -= 1 * Time.deltaTime;

            weapon.transform.localRotation = Quaternion.Euler(rotationAmount);

            yield return null;
        }
        transform.localRotation = Quaternion.identity;
        isRecoiling = false;
    }

    private IEnumerator ShakeWeapon(GameObject weapon) {
        isShaking = true;

        while (shakeDuration > 0.01f) {
            Vector3 rotationAmount = Random.insideUnitSphere * shakeValue;
            rotationAmount.z = 0;

            shakePercentage = shakeDuration / startDuration;

            shakeValue = startValue * shakePercentage;
            //shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);//lerpa eller inte?
            shakeDuration -= 1 * Time.deltaTime;

            if (isSmooth) {
                weapon.transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothValue);
            } else {
                weapon.transform.localRotation = Quaternion.Euler(rotationAmount);
            }

            yield return null;
        }
        transform.localRotation = Quaternion.identity;
        isShaking = false;
    }

}
