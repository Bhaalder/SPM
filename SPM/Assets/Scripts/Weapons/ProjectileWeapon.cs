using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : BaseWeapon{
    //Author: Patrik Ahlgren
    private float projectileSpeed;
    private GameObject projectile;

    public ProjectileWeapon(string name, float damage, float range, float fireRate, float reloadTime, float impactForce, float spread, float projectileSpeed, int ammoInClip, int maxAmmoInClip, int totalAmmoLeft, GameObject projectile, Sprite crosshair): base(name, damage, range, fireRate, reloadTime, impactForce, spread, ammoInClip, maxAmmoInClip, totalAmmoLeft, crosshair) {
        this.projectileSpeed = projectileSpeed;
        this.projectile = projectile;
    }
    public float GetProjectileSpeed() {
        return projectileSpeed;
    }
    public void SetProjectileSpeed(float f) {
        projectileSpeed = f;
    }
    public GameObject GetProjectile() {
        return projectile;
    }
    public void SetProjectile(GameObject go) {
        projectile = go;
    }
}
