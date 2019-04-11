using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public Camera cam;

    [SerializeField] private Transform camFocusTrans;

    private void Start(){

    }

    void Update(){
        transform.LookAt(camFocusTrans);
    }

    public void StartShooting(Weapon weapon) {
        //ska lägga in ifall man skjuter rocketlauncher
        if (weapon is ProjectileWeapon) {
            ShootProjectile(weapon);
        } else {ShootHitScan(weapon);}
            
    }

    void ShootHitScan(Weapon weapon) {

        if (weapon.GetAmmoInClip() != 0) {
            weapon.DecreaseAmmoInClip();
            bool hitTarget = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, weapon.GetRange());
            if (hitTarget) {
                Debug.Log("Hit " + hit.transform.name);
                if (hit.collider.gameObject.layer == 9) { hit.transform.GetComponent<EnemyController>().TakeDamage(weapon.GetDamage()); }
            }
            if (!hitTarget) {
                Debug.Log("Miss!");
            }
        } else if (weapon.GetAmmoInClip() <= 0) {
            Debug.Log("Out of Ammo");
        }
    }

    void ShootProjectile(Weapon weapon) {
        if (weapon.GetAmmoInClip() != 0) {
            weapon.DecreaseAmmoInClip();
            bool hitTarget = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, weapon.GetRange());
            if (hitTarget) {
                Debug.Log("Projetile moving towards " + hit.transform.name);
                //skapa en projektil som ska träffa istället

                //if (hit.collider.gameObject.layer == 9) { hit.transform.GetComponent<EnemyController>().TakeDamage(weapon.GetDamage()); }
            }
            if (!hitTarget) {
                Debug.Log("Projetile moving towards nothing");
            }
        } else if (weapon.GetAmmoInClip() <= 0) {
            Debug.Log("Out of Ammo");
        }
    }
}