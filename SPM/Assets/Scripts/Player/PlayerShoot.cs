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
            ShootHitScan(weapon);
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

    void ShootProjectile() {
        Debug.Log("PROJECTILE");
    }
}