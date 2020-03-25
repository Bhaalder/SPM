﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    //Author: Patrik Ahlgren

    [SerializeField] private string weaponName;
    [SerializeField] private Animator anim;

    private void Start()
    {
        anim = GameObject.Find("WeaponText").GetComponent<Animator>();
    }

    public void GetWeapon() {
        BaseWeapon weaponPickup = null;
        anim.SetTrigger("WeaponPickUp");
        if (weaponName == "Rifle") {
            weaponPickup = WeaponController.Instance.GetRifle();
        }
        if (weaponName == "Shotgun") {
            weaponPickup = WeaponController.Instance.GetShotgun();
            Invoke(GameController.Instance.TipMethod("Click 1 to 2 to switch between weapons!"), 4);
        }
        if (weaponName == "RocketLauncher") {
            weaponPickup = WeaponController.Instance.GetRocketLauncher();
            Invoke(GameController.Instance.TipMethod("Click 1 to 3 to switch between weapons!"), 4);
            
        }
        if (weaponPickup != null) {
            foreach (BaseWeapon weapon in GameController.Instance.PlayerWeapons) {
                if (weaponPickup.GetName() == weapon.GetName()) {
                    Destroy(gameObject);
                    return;
                }
            }
            GameController.Instance.PlayerWeapons.Add(weaponPickup);
            GameController.Instance.Player.GetComponent<PlayerInput>().AbortReload();
            GameController.Instance.Player.GetComponent<PlayerInput>().SwitchWeaponAnimation(weaponPickup);
            GameController.Instance.SelectedWeapon = weaponPickup;
            GameController.Instance.UpdateSelectedWeapon();
        }
        Destroy(gameObject);
    }
}
