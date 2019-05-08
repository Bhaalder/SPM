using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //Author: Patrik Ahlgren
    public GameObject rocketLaucherProjectileGO;
    public GameObject enemyWeaponProjectileGO;
     
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
        //////////////////////////////////Name____dmg_range_FR_reload_Impact_spr_aic_maic_totalammoleft
        BaseWeapon rifle = new BaseWeapon("Rifle", 10, 100, 10f, 0.6f, 0.1f, 15, 50, 50, 500, null, null, null);
        return rifle;
    }

    public BaseWeapon GetShotgun() {
        /////////////////////////////////////Name____dmg_range_FR_reload_Impact_spr_aic_maic_totalammoleft
        BaseWeapon shotgun = new BaseWeapon("Shotgun", 30, 18, 2f, 0.5f, 0.1f, 30, 5, 5, 200, null, null, null);
        return shotgun;
    }

    public ProjectileWeapon GetRocketLauncher() {
        /////////////////////////////////////////////////////////////////Name____dmg_range_FR_reload_Impact_spr_aic_maic_totalammoleft
        ProjectileWeapon rocketLauncher = new ProjectileWeapon("Rocket Launcher", 30, 100, 1f, 0.3f, 0.01f, 20, 10, 5, 5, 50, rocketLaucherProjectileGO, null, null, null);
        return rocketLauncher;
    }

    public ProjectileWeapon GetEnemyProjectileWeapon() {
        /////////////////////////////////////////////////////////////////Name___dmg_range_FR_reload_Impact_spr_PS_aic_maic_totalammoleft
        ProjectileWeapon rocketLauncher = new ProjectileWeapon("Enemy Projectile", 30, 50, 2f, 0.3f, 0.1f, 20, 5, 99999, 99999, 99999, enemyWeaponProjectileGO, null, null, null);
        return rocketLauncher;
    }

}
