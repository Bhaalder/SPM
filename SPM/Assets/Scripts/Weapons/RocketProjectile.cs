using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour{
    //Main author: Patrik Ahlgren
    //Secondary author: Fredrik
    [SerializeField] private LayerMask layerMask;

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
            Destroy(gameObject);
            GetComponent<Explosion>().Explode(projectileForce, projectileDamage);
        }
        if (hitTarget) {
            distanceToTarget = Vector3.Distance(transform.position, hit.point);
        } else if (distanceToTarget == 10000) {
            Destroy(gameObject, 10f);
        }
        if (GameController.Instance.GameIsSlowmotion && !GameController.Instance.GameIsPaused) {
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
}
