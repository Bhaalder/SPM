using System.Collections;
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
            Debug.Log("SHOTGUN PICKUP");
            weaponPickup = WeaponController.Instance.GetShotgun();
            StartCoroutine(TextShowDelay("SHOTGUN INFO"));
        }
        if (weaponName == "RocketLauncher") {
            weaponPickup = WeaponController.Instance.GetRocketLauncher();
            StartCoroutine(TextShowDelay("ROCKET INFO"));

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

    private IEnumerator TextShowDelay(string text)
    {
        Debug.Log("num started");
        GameController.Instance.TipMethod(text);
        yield return new WaitForSeconds(1);
        
        
        Debug.Log("weaponText identified");
        //yield return null;
        

        

    }
}
