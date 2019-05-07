using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionRadius;
    public GameObject explosionEffect;

    private GameObject explosion;
    
    public void Explode(float explosionForce, float damage) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.gameObject.layer == 9)
            {
                nearbyObject.transform.GetComponent<EnemyController>().TakeDamage(damage);
            }
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(explosion, 4f);
        
    }
}
