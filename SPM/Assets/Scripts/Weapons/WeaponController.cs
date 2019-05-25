using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour{
    //Author: Patrik Ahlgren
    [SerializeField] private GameObject rocketLaucherProjectileGO;
    [SerializeField] private GameObject enemyWeaponProjectileGO;
    [SerializeField] private Sprite[] crosshair;

    [SerializeField] private float RifleDmg;
    [SerializeField] private float ShotgunDmg;
    [SerializeField] private float RocketLDmg;


    private static WeaponController _instance;

    public static WeaponController Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<WeaponController>();
#if UNITY_EDITOR
                if (FindObjectsOfType<WeaponController>().Length > 1) {
                    Debug.LogError("Found more than one weaponcontroller");
                }
#endif
            }
            return _instance;
        }
    }

    public BaseWeapon GetRifle() {
        BaseWeapon rifle = new BaseWeapon("Rifle", RifleDmg, 150, 9f, 1.6f, 0.1f, 15, 50, 50, 500, crosshair[0]);
        return rifle;
    }

    public BaseWeapon GetShotgun() {
        BaseWeapon shotgun = new BaseWeapon("Shotgun", ShotgunDmg, 30, 2f, 2f, 0.1f, 30, 8, 8, 200, crosshair[1]);
        return shotgun;
    }

    public ProjectileWeapon GetRocketLauncher() {
        ProjectileWeapon rocketLauncher = new ProjectileWeapon("Rocket Launcher", RocketLDmg, 100, 1.15f, 3.3f, 0.01f, 20, 15, 3, 3, 15, rocketLaucherProjectileGO, crosshair[2]);
        return rocketLauncher;
    }

    public ProjectileWeapon GetEnemyProjectileWeapon() {
        ProjectileWeapon enemyWeapon = new ProjectileWeapon("Enemy Projectile", 10, 50, 2f, 0.3f, 0.1f, 20, 20, 99999, 99999, 99999, enemyWeaponProjectileGO, null);
        return enemyWeapon;
    }

}
