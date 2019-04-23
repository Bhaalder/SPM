using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public GameObject rocketLaucherProjectileGO;

    private static WeaponController instance;

    public static WeaponController Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<WeaponController>();
#if UNITY_EDITOR
                if (FindObjectsOfType<WeaponController>().Length > 1) {
                    Debug.LogError("Found more than one weaponcontroller");
                }
#endif
            }
            return instance;
        }
    }

    public BaseWeapon GetRifle() {
        BaseWeapon rifle = new BaseWeapon("Rifle", 10, 100, 10f, 0.5f, 0.1f, 15, 50, 50, 999, null, null, null);
        return rifle;
    }

    public BaseWeapon GetShotgun() {
        BaseWeapon shotgun = new BaseWeapon("Shotgun", 30, 18, 2f, 0.5f, 0.1f, 30, 25, 25, 100, null, null, null);
        return shotgun;
    }

    public ProjectileWeapon GetRocketLauncher() {
        ProjectileWeapon rocketLauncher = new ProjectileWeapon("Rocket Launcher", 30, 100, 1f, 0.3f, 0.01f, 20, 10, 5, 5, 100, rocketLaucherProjectileGO, null, null, null);
        return rocketLauncher;
    }


}
