using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{

    public int ammoIncrease;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            foreach (BaseWeapon weapon in GameController.Instance.playerWeapons) {
                weapon.IncreaseTotalAmmoLeft(ammoIncrease);
            }
            Destroy(gameObject);
        }
    }
}
