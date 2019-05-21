using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour{
    //Main author: Fredrik
    //Secondary author: Patrik Ahlgren
    public LayerMask layerMask;

    private float projectileSpeed;
    private float projectileDamage;
    private float projectileForce;
    private GameObject rocketSound;
    private float distanceToTarget = 10000;

    private void Awake() {
        rocketSound = AudioController.Instance.Play_RandomPitch_InWorldspace("RocketLauncher_Rocket", gameObject, 0.95f, 1f);
        rocketSound.transform.SetParent(gameObject.transform);
    }

    private void Update(){
        bool hitTarget = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distanceToTarget, layerMask);
        if (distanceToTarget <= 0.5f) {
            GetComponent<Explosion>().Explode(projectileForce, projectileDamage);
            Destroy(gameObject);
        }
        if (hitTarget) {
            distanceToTarget = Vector3.Distance(transform.position, hit.point);
        } else if (!hitTarget) {
            Destroy(gameObject, 10f);
        }
        if (GameController.Instance.gameIsSlowmotion) {
            transform.position += transform.forward * (projectileSpeed/2f) * Time.unscaledDeltaTime;
            IncreaseSpeed();
        } else {
            transform.position += transform.forward * projectileSpeed * Time.deltaTime;
            IncreaseSpeed();
        }
        Debug.DrawLine(transform.position, hit.point, Color.red);
    }

    private void IncreaseSpeed(){
        if (projectileSpeed < 25){
            projectileSpeed *= 1.05f;
        }
    }

    public void SetProjectileSpeed(float speed){
        projectileSpeed = speed;
    }
    public void SetProjectileDamage(float damage){
        projectileDamage = damage;
    }
    public void SetProjectileForce(float force){
        projectileForce = force;
    }

    //private void OnCollisionEnter(Collision collision){
    //    GetComponent<Explosion>().Explode(projectileForce, projectileDamage);
    //    Destroy(gameObject);
    //}
}
