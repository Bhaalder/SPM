using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    private float projectileSpeed;
    public ProjectileWeapon(string name, float damage, float range, float fireRate, float reloadTime, float impactForce, float projectileSpeed, int ammoInClip, int maxAmmoInClip, int totalAmmoLeft, AudioClip reloadSound, AudioClip shootSound, AudioClip noAmmoSound) : base(name, damage, range, fireRate, reloadTime, impactForce, ammoInClip, maxAmmoInClip, totalAmmoLeft, reloadSound, shootSound, noAmmoSound) {
        this.projectileSpeed = projectileSpeed;
    }

    public float GetProjectileSpeed() {
        return projectileSpeed;
    }
    public void SetProjectileSpeed(float f) {
        projectileSpeed = f;
    }
}
