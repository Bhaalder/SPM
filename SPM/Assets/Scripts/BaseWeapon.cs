using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon{
    private string name;
    private float damage;
    private float range;
    private float fireRate;
    private float reloadTime;
    private float impactForce;
    private float spread;
    private int ammoInClip;
    private int maxAmmoInClip;
    private int totalAmmoLeft;
    private AudioClip reloadSound;
    private AudioClip shootSound;
    private AudioClip noAmmoSound;

    public BaseWeapon (string name, float damage, float range, float fireRate, float reloadTime, float impactForce, float spread, int ammoInClip, int maxAmmoInClip, int totalAmmoLeft,
        AudioClip reloadSound, AudioClip shootSound, AudioClip noAmmoSound) {
        this.name = name;
        this.damage = damage;
        this.range = range;
        this.fireRate = fireRate;
        this.reloadTime = reloadTime;
        this.impactForce = impactForce;
        this.spread = spread;
        this.ammoInClip = ammoInClip;
        this.maxAmmoInClip = maxAmmoInClip;
        this.totalAmmoLeft = totalAmmoLeft;
        this.reloadSound = reloadSound;
        this.shootSound = shootSound;
        this.noAmmoSound = noAmmoSound;
    }

    public string GetName() {
        return name;
    }
    public void SetName(string s) {
        name = s;
    }
    public float GetDamage() {
        return damage;
    }
    public void SetDamage(float f) {
        damage = f;
    }
    public float GetRange() {
        return range;
    }
    public void SetRange(float f) {
        range = f;
    }
    public float GetFireRate() {
        if(Time.timeScale < 1) {
            return fireRate * 5;
        }    //för slowmotion, borde finnas en bättre lösning
        return fireRate;
    }
    public void SetFireRate(float f) {
        fireRate = f;
    }
    public float GetReloadTime() {
        if (Time.timeScale < 1) {
            return reloadTime * 10;
        }   //för slowmotion, borde finnas en bättre lösning
        return reloadTime;
    }
    public void SetReloadTime(float f) {
        reloadTime = f;
    }
    public float GetImpactForce() {
        return impactForce;
    }
    public void SetImpactForce(float f) {
        impactForce = f;
    }
    public float GetSpread() {
        return spread;
    }
    public void SetSpread(float f) {
        spread = f;
    }
    public int GetAmmoInClip() {
        return ammoInClip;
    }
    public void SetAmmoInClip(int i) {
        ammoInClip = i;
    }
    public void DecreaseAmmoInClip() {
        ammoInClip--;
    }
    public int GetMaxAmmoInClip() {
        return maxAmmoInClip;
    }
    public void SetMaxAmmoInClip(int i) {
        maxAmmoInClip = i;
    }
    public int GetTotalAmmoLeft() {
        return totalAmmoLeft;
    }
    public void SetTotalAmmoLeft(int i) {
        totalAmmoLeft = i;
    }
    public void IncreaseTotalAmmoLeft(int i) {
        totalAmmoLeft += i;
    }
    public AudioClip GetReloadSound() {
        return reloadSound;
    }
    public void SetReloadSound(AudioClip audio) {
        reloadSound = audio;
    }
    public AudioClip GetShootSound() {
        return shootSound;
    }
    public void SetShootSound(AudioClip audio) {
        shootSound = audio;
    }
    public AudioClip GetNoAmmoSound() {
        return noAmmoSound;
    }
    public void SetNoAmmoSound(AudioClip audio) {
        noAmmoSound = audio;
    }
}
