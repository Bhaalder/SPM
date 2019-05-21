using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour{
    //Author: Patrik Ahlgren
    public GameObject rocketLaucherProjectileGO;
    public GameObject enemyWeaponProjectileGO;
    public Sprite[] crosshair;

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
        BaseWeapon rifle = new BaseWeapon("Rifle", 7.5f, 150, 9f, 1.6f, 0.1f, 15, 50, 50, 500, crosshair[0]);
        return rifle;
    }

    public BaseWeapon GetShotgun() {
        BaseWeapon shotgun = new BaseWeapon("Shotgun", 25, 30, 2f, 2f, 0.1f, 30, 8, 8, 200, crosshair[1]);
        return shotgun;
    }

    public ProjectileWeapon GetRocketLauncher() {
        ProjectileWeapon rocketLauncher = new ProjectileWeapon("Rocket Launcher", 30, 100, 1.15f, 3.3f, 0.01f, 20, 15, 3, 3, 15, rocketLaucherProjectileGO, crosshair[2]);
        return rocketLauncher;
    }

    public ProjectileWeapon GetEnemyProjectileWeapon() {
        ProjectileWeapon enemyWeapon = new ProjectileWeapon("Enemy Projectile", 30, 50, 2f, 0.3f, 0.1f, 20, 5, 99999, 99999, 99999, enemyWeaponProjectileGO, null);
        return enemyWeapon;
    }

}
