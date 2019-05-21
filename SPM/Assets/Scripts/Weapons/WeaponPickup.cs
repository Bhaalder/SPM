using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    //Author: Patrik Ahlgren

    public GameObject weaponPickupPrefab;
    private MeshFilter meshFilter;
    private Mesh mesh;

    private void Start() {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        mesh = gameObject.GetComponent<Mesh>();
        meshFilter = weaponPickupPrefab.GetComponent<MeshFilter>();
        mesh = weaponPickupPrefab.GetComponent<Mesh>();
        gameObject.transform.localScale = weaponPickupPrefab.transform.localScale;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "InteractionPlayer") {
            BaseWeapon weaponPickup = null;
            if (weaponPickupPrefab.name == "Weapon_Rifle") {
                weaponPickup = WeaponController.Instance.GetRifle();             
            }
            if (weaponPickupPrefab.name == "Weapon_Shotgun") {
                weaponPickup = WeaponController.Instance.GetShotgun();
            }
            if (weaponPickupPrefab.name == "Weapon_RocketLauncher") {
                weaponPickup = WeaponController.Instance.GetRocketLauncher();                
            }
            if (weaponPickup != null) {
                foreach(BaseWeapon weapon in GameController.Instance.playerWeapons) {
                    if(weaponPickup.GetName() == weapon.GetName()) {
                        Destroy(gameObject);
                        return;
                    }
                }
                GameController.Instance.playerWeapons.Add(weaponPickup);
                GameController.Instance.player.GetComponent<PlayerInput>().AbortReload();
                GameController.Instance.selectedWeapon = weaponPickup;
                GameController.Instance.UpdateSelectedWeapon();
            }
            Destroy(gameObject);
        }
    }
}
