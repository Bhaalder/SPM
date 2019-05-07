using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{

    public LayerMask layerMask;

    [SerializeField] public Transform weaponFocus;

    [SerializeField] private GameObject bulletImpactMetalGO;

    [SerializeField] private GameObject bulletImpactAlienGO;

    [SerializeField] private ParticleSystem muzzleFlash;

    private GameObject bulletImpact;

    private void Start(){
        weaponFocus = GameObject.Find("WeaponFocus").transform;
    }

    void Update(){
        transform.LookAt(weaponFocus);
    }

    public void Melee() {
        bool hitTarget = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 5f, layerMask);
        if (hitTarget) {
            if (hit.rigidbody != null) {
                hit.rigidbody.AddForce(-hit.normal * 10f);
            }
            if (hit.collider.gameObject.layer == 9) {
                hit.transform.GetComponent<EnemyController>().TakeDamage(100f);
            }
            bulletImpact = Instantiate(bulletImpactMetalGO, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(bulletImpact, 2.0f);
        }
    }

    public void StartShooting(BaseWeapon weapon) {
        if (weapon is ProjectileWeapon) {
            ShootProjectile((ProjectileWeapon) weapon);
        }
        else if (weapon.GetName().Equals("Shotgun")){
            ShootShotgunHitScan(weapon);
            
        }            
        else {
            ShootHitScan(weapon);
            
        }            
    }

    private void ShootHitScan(BaseWeapon weapon) {

        if (weapon.GetAmmoInClip() != 0) {
            muzzleFlash.Play();
            weapon.DecreaseAmmoInClip();
            bool hitTarget = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, weapon.GetRange(), layerMask);

            // få in spread på något sätt
            if (hitTarget) {
                if (hit.rigidbody != null) {
                    hit.rigidbody.AddForce(-hit.normal * weapon.GetImpactForce());
                }
                if (hit.collider.gameObject.layer == 9){
                    hit.transform.GetComponent<EnemyController>().TakeDamage(weapon.GetDamage());
                    bulletImpact = Instantiate(bulletImpactAlienGO, hit.point, Quaternion.LookRotation(hit.normal));
                } else {
                    bulletImpact = Instantiate(bulletImpactMetalGO, hit.point, Quaternion.LookRotation(hit.normal));
                }
                Destroy(bulletImpact, 2.0f);
            }
        } else if (weapon.GetAmmoInClip() <= 0) {
            Debug.Log("Out of Ammo");
        }
    }

    //TEST
    private void ShootShotgunHitScan(BaseWeapon weapon){
        if (weapon.GetAmmoInClip() != 0){
            muzzleFlash.Play();
            weapon.DecreaseAmmoInClip();
            bool[] hitTarget = new bool[5];
            RaycastHit[] hits = new RaycastHit[5];
            for(int i = 0; i < hitTarget.Length; i++){
                float rndX = Random.Range(-0.1f, 0.1f), rndY = Random.Range(-0.1f, 0.1f);
                hitTarget[i] = Physics.Raycast(transform.position, transform.forward + new Vector3(rndX, rndY, 0), out hits[i], weapon.GetRange(), layerMask);
            }
            for(int x = 0; x < hitTarget.Length; x++){
                if (hitTarget[x]){
                    if (hits[x].rigidbody != null){
                        hits[x].rigidbody.AddForce(-hits[x].normal * weapon.GetImpactForce());
                    }
                    if (hits[x].collider.gameObject.layer == 9){
                        float fallOff = (hits[x].distance * (hits[x].distance / 2)) / 4;
                        Debug.Log(weapon.GetDamage() - fallOff);
                        if(fallOff > weapon.GetDamage()){
                            fallOff = weapon.GetDamage();
                        }
                        hits[x].transform.GetComponent<EnemyController>().TakeDamage(weapon.GetDamage() - fallOff);
                        bulletImpact = Instantiate(bulletImpactAlienGO, hits[x].point, Quaternion.LookRotation(hits[x].normal));
                    } else {
                        bulletImpact = Instantiate(bulletImpactMetalGO, hits[x].point, Quaternion.LookRotation(hits[x].normal));
                    }
                    
                    Destroy(bulletImpact, 2.0f);
                }
            }
        }
        else if (weapon.GetAmmoInClip() <= 0){
            Debug.Log("Out of Ammo");
        }
    }

    private void ShootProjectile(ProjectileWeapon weapon) {
        if (weapon.GetAmmoInClip() != 0) {
            weapon.DecreaseAmmoInClip();

            GameObject rocketProj = Instantiate(weapon.GetProjectile(), transform.position + transform.forward * 2, Quaternion.identity);
            rocketProj.transform.LookAt(weaponFocus);
            rocketProj.GetComponent<RocketProjectile>().SetProjectileSpeed(weapon.GetProjectileSpeed());
            rocketProj.GetComponent<RocketProjectile>().SetProjectileForce(weapon.GetImpactForce());
            rocketProj.GetComponent<RocketProjectile>().SetProjectileDamage(weapon.GetDamage());

        } else if (weapon.GetAmmoInClip() <= 0) {
            Debug.Log("Out of Ammo");
        }
    }
    
}