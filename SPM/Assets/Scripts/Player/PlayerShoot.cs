using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour{
    //Author: Patrik Ahlgren

    public LayerMask layerMask;
    
    [SerializeField] private GameObject bulletImpactMetalGO;
    [SerializeField] private GameObject bulletImpactMetalSGGO;
    [SerializeField] private GameObject bulletImpactAlienGO;

    [SerializeField] private ParticleSystem muzzleFlash;//------------

    [SerializeField] private float shotgunRecoil = 4;// TA BORT SEN
    [SerializeField] private float shotgunRecoilDuration = 0.3f; // TA BORT SEN

    private CameraShake camShake;
    private GameObject bulletImpact;
    private float alienWoundTimer = 0.2f;

    private void Start(){
        camShake = Camera.main.GetComponent<CameraShake>();
    }

    //public void Melee() {
    //    bool hitTarget = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 5f, layerMask);
    //    if (hitTarget) {
    //        if (hit.rigidbody != null) {
    //            hit.rigidbody.AddForce(-hit.normal * 10f);
    //        }
    //        if (hit.collider.gameObject.layer == 9) {
    //            hit.transform.GetComponent<Enemy>().TakeDamage(100f);
    //        }
    //        InstantiateSingleBulletHit(bulletImpactMetalGO, hit, 2.0f);
    //    }
    //}

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
            AudioController.Instance.PlaySFX_RandomPitch("Rifle", 0.92f, 1f);
            muzzleFlash.Play();//----------------Animation
            weapon.DecreaseAmmoInClip();
            bool hitTarget = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, weapon.GetRange(), layerMask);

            if (hitTarget) {
                if (hit.rigidbody != null && hit.collider.gameObject.layer != 2) {
                    hit.rigidbody.AddForce(-hit.normal * weapon.GetImpactForce());
                }
                if (hit.collider.gameObject.layer == 9){
                    GameController.Instance.ShowHitmark(0.2f);
                    hit.transform.GetComponent<Enemy>().TakeDamage(weapon.GetDamage());
                    InstantiateSingleBulletHit(bulletImpactAlienGO, hit, alienWoundTimer);
                } else {
                    InstantiateSingleBulletHit(bulletImpactMetalGO, hit, 2.0f);
                }
            }
        } else if (weapon.GetAmmoInClip() <= 0) {
            Debug.Log("Out of Ammo");
        }
        camShake.Shake(1f, 0.4f);
    }

    private void ShootShotgunHitScan(BaseWeapon weapon){
        if (weapon.GetAmmoInClip() != 0){           
            AudioController.Instance.PlaySFX_RandomPitch("Shotgun", 0.95f, 1f);
            muzzleFlash.Play();//------------Animation
            weapon.DecreaseAmmoInClip();
            bool[] hitTarget = new bool[15];
            RaycastHit[] hits = new RaycastHit[15];
            for(int i = 0; i < hitTarget.Length; i++){
                float rndX = Random.Range(-0.1f, 0.1f), rndY = Random.Range(-0.03f, 0.03f);
                hitTarget[i] = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward + new Vector3(rndX, rndY, 0), out hits[i], weapon.GetRange(), layerMask);
            }
            for(int x = 0; x < hitTarget.Length; x++){
                if (hitTarget[x]){
                    if (hits[x].rigidbody != null && hits[x].collider.gameObject.layer != 2) {
                        hits[x].rigidbody.AddForce(-hits[x].normal * weapon.GetImpactForce());
                    }
                    if (hits[x].collider.gameObject.layer == 9){

                        float fallOff = Vector3.Distance(GameController.Instance.Player.transform.position, hits[x].point);
                        
                        if(fallOff > weapon.GetDamage()){
                            fallOff = weapon.GetDamage();
                        } else if (weapon.GetDamage() - fallOff < 0) {
                            fallOff = weapon.GetDamage();
                        }
                        float damage = weapon.GetDamage() - fallOff;
                        GameController.Instance.ShowHitmark(0.5f);
                        hits[x].transform.GetComponent<Enemy>().TakeDamage(damage);
                        InstantiateMultipleBulletHits(bulletImpactAlienGO, hits, x, alienWoundTimer);                       
                    } else {
                        InstantiateMultipleBulletHits(bulletImpactMetalSGGO, hits, x, 2.0f);
                    }               
                }
            }
            camShake.RecoilShake(shotgunRecoil, shotgunRecoilDuration);
            camShake.Shake(shotgunRecoil, 0.5f);
        }
        else if (weapon.GetAmmoInClip() <= 0){
            Debug.Log("Out of Ammo");
        }
    }

    private void ShootProjectile(ProjectileWeapon weapon) {
        if (weapon.GetAmmoInClip() != 0) {
            
            AudioController.Instance.PlaySFX_RandomPitch("RocketLauncher_Launch", 0.95f, 1f);
            weapon.DecreaseAmmoInClip();

            GameObject rocketProj = Instantiate(weapon.GetProjectile(), Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.rotation);
            rocketProj.GetComponent<RocketProjectile>().SetProjectileSpeed(weapon.GetProjectileSpeed());
            rocketProj.GetComponent<RocketProjectile>().SetProjectileForce(weapon.GetImpactForce());
            rocketProj.GetComponent<RocketProjectile>().SetProjectileDamage(weapon.GetDamage());

        } else if (weapon.GetAmmoInClip() <= 0) {
            Debug.Log("Out of Ammo");
        }
        camShake.RecoilShake(4, 0.3f);
        camShake.Shake(2, 0.5f);
    }
    
    private void InstantiateMultipleBulletHits(GameObject impactGO, RaycastHit[] hits, int numberOfHits, float timeUntilDestroy) {
        bulletImpact = Instantiate(impactGO, hits[numberOfHits].point, Quaternion.LookRotation(hits[numberOfHits].normal));
        Destroy(bulletImpact, timeUntilDestroy);
    }
    private void InstantiateSingleBulletHit(GameObject impactGO, RaycastHit hit, float timeUntilDestroy) {
        bulletImpact = Instantiate(impactGO, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(bulletImpact, timeUntilDestroy);
    }

}