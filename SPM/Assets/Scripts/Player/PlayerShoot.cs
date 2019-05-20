using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour{
    //Author: Patrik Ahlgren


        /*
         * MUZZLEFLASH BORDE LIGGA TILLSAMMANS MED VAPEN OCH DET HÄR BORDE BARA SÄGA TILL VAPNEN ATT SPELA ANIMATION
         * */
    public LayerMask layerMask;
    
    [SerializeField] private GameObject bulletImpactMetalGO;

    [SerializeField] private GameObject bulletImpactAlienGO;

    [SerializeField] private ParticleSystem muzzleFlash;//------------

    public float shotgunRecoil = 4;// TA BORT SEN
    public float shotgunRecoilDuration = 0.3f; // TA BORT SEN

    private CameraShake camShake;
    private GameObject bulletImpact;
    private float alienWoundTimer = 0.2f;

    private void Start(){
        camShake = Camera.main.GetComponent<CameraShake>();
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
            InstantiateSingleBulletHit(bulletImpactMetalGO, hit, 2.0f);
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
            AudioController.Instance.Play_RandomPitch("Rifle", 0.92f, 1f);
            muzzleFlash.Play();//----------------Animation
            weapon.DecreaseAmmoInClip();
            bool hitTarget = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, weapon.GetRange(), layerMask);

            if (hitTarget) {
                if (hit.rigidbody != null && hit.collider.gameObject.layer != 2) {
                    hit.rigidbody.AddForce(-hit.normal * weapon.GetImpactForce());
                }
                if (hit.collider.gameObject.layer == 9){
                    GameController.Instance.Hitmark();
                    hit.transform.GetComponent<EnemyController>().TakeDamage(weapon.GetDamage());
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

    //TEST
    private void ShootShotgunHitScan(BaseWeapon weapon){
        if (weapon.GetAmmoInClip() != 0){           
            AudioController.Instance.Play_RandomPitch("Shotgun", 0.95f, 1f);
            muzzleFlash.Play();//------------Animation
            weapon.DecreaseAmmoInClip();
            bool[] hitTarget = new bool[5];
            RaycastHit[] hits = new RaycastHit[5];
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
                        float fallOff = (hits[x].distance * (hits[x].distance / 2)) / 6;
                        Debug.Log(weapon.GetDamage() - fallOff);
                        if(fallOff > weapon.GetDamage()){
                            fallOff = weapon.GetDamage();
                        }
                        
                        hits[x].transform.GetComponent<EnemyController>().TakeDamage(weapon.GetDamage() - fallOff);
                        InstantiateMultipleBulletHits(bulletImpactAlienGO, hits, x, alienWoundTimer);                       
                    } else {
                        InstantiateMultipleBulletHits(bulletImpactMetalGO, hits, x, 2.0f);
                    }
                    
                }
            }
            camShake.RecoilShake(shotgunRecoil, shotgunRecoilDuration);
        }
        else if (weapon.GetAmmoInClip() <= 0){
            Debug.Log("Out of Ammo");
        }
    }

    private void ShootProjectile(ProjectileWeapon weapon) {
        if (weapon.GetAmmoInClip() != 0) {
            
            AudioController.Instance.Play_RandomPitch("RocketLauncher_Launch", 0.95f, 1f);
            weapon.DecreaseAmmoInClip();

            GameObject rocketProj = Instantiate(weapon.GetProjectile(), Camera.main.transform.position + (Camera.main.transform.forward*2), Camera.main.transform.rotation);
            rocketProj.GetComponent<RocketProjectile>().SetProjectileSpeed(weapon.GetProjectileSpeed());
            rocketProj.GetComponent<RocketProjectile>().SetProjectileForce(weapon.GetImpactForce());
            rocketProj.GetComponent<RocketProjectile>().SetProjectileDamage(weapon.GetDamage());

        } else if (weapon.GetAmmoInClip() <= 0) {
            Debug.Log("Out of Ammo");
        }
        camShake.RecoilShake(4, 0.3f);
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