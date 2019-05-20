using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour{
    //Main author: Fredrik
    //Secondary author: Patrik Ahlgren

    private float projectileSpeed;
    private float projectileDamage;
    private float projectileForce;
    private GameObject rocketSound;

    private void Awake() {
        rocketSound = AudioController.Instance.Play_RandomPitch_InWorldspace("RocketLauncher_Rocket", gameObject, 0.95f, 1f);
        rocketSound.transform.SetParent(gameObject.transform);
    }

    private void Update(){
        if (GameController.Instance.gameIsSlowmotion) {
            transform.position += transform.forward * (projectileSpeed/2) * Time.unscaledDeltaTime;
            IncreaseSpeed();
        } else {
            transform.position += transform.forward * projectileSpeed * Time.deltaTime;
            IncreaseSpeed();
        }       
    }

    private void IncreaseSpeed(){
        if (projectileSpeed < 30)
        {
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

    private void OnCollisionEnter(Collision collision){
        GetComponent<Explosion>().Explode(projectileForce, projectileDamage);
        Destroy(gameObject);
    }
}
