using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private Transform camFocusTrans;

    [SerializeField] private GameObject rocketGO;

    private void Start(){

    }

    void Update(){
        transform.LookAt(camFocusTrans);
    }

    public void StartShooting(Weapon weapon) {
        if (weapon is ProjectileWeapon) {
            ShootProjectile((ProjectileWeapon) weapon);
        } else {ShootHitScan(weapon);}
            
    }

    private void ShootHitScan(Weapon weapon) {

        if (weapon.GetAmmoInClip() != 0) {
            weapon.DecreaseAmmoInClip();
            bool hitTarget = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, weapon.GetRange());
            if (hitTarget) {
                if (hit.rigidbody != null) {
                    hit.rigidbody.AddForce(-hit.normal * weapon.GetImpactForce());
                }
                Debug.Log("Hit " + hit.transform.name);
                if (hit.collider.gameObject.layer == 9){
                    hit.transform.GetComponent<EnemyController>().TakeDamage(weapon.GetDamage());
                }
            }
            if (!hitTarget) {
                Debug.Log("Miss!");
            }
        } else if (weapon.GetAmmoInClip() <= 0) {
            Debug.Log("Out of Ammo");
        }
    }

    private void ShootProjectile(ProjectileWeapon weapon) {
        if (weapon.GetAmmoInClip() != 0) {
            weapon.DecreaseAmmoInClip();

            GameObject rocketProj = Instantiate(rocketGO, transform.position + transform.forward * 2, Quaternion.identity);
            rocketProj.transform.LookAt(camFocusTrans);
            rocketProj.transform.position += transform.forward * weapon.GetProjectileSpeed() * (Time.deltaTime * Time.deltaTime);

        } else if (weapon.GetAmmoInClip() <= 0) {
            Debug.Log("Out of Ammo");
        }
    }
}