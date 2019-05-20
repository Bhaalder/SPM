using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //Author: Patrik Ahlgren
    public float explosionRadius;
    public GameObject explosionEffect;
    public GameObject fireEffect;

    private GameObject explosion;
    private GameObject fire;
    
    public void Explode(float explosionForce, float damage) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.gameObject.layer == 9)
            {
                nearbyObject.transform.GetComponent<EnemyController>().TakeDamage(damage);
            }
            Rigidbody rigidBody = nearbyObject.GetComponent<Rigidbody>();
            if (rigidBody != null){
                rigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraShake>().ShakeIncreaseDistance(25f, 1.5f, GameController.Instance.player, gameObject);
        AudioController.Instance.Play_RandomPitch_InWorldspace("Explosion", gameObject, 0.95f, 1f);
        fire = Instantiate(fireEffect, transform.position, Quaternion.identity);
        Destroy(explosion, 3.75f);
        Destroy(fire, 10f);
    }
}
