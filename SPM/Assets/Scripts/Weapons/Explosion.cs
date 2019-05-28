using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //Author: Patrik Ahlgren
    [SerializeField] private float explosionRadius;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject fireEffect;

    private GameObject explosion;
    private GameObject fire;

    public bool InstantiateFire;

    public void Explode(float explosionForce, float damage) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.gameObject.layer == 9){
                GameController.Instance.ShowHitmark(0.5f);
                float damageDropoff = Vector3.Distance(transform.position, nearbyObject.transform.position)*2.5f;
                if (damageDropoff > damage) {
                    damageDropoff = (damage-1);
                }
                float finalDamage = damage - damageDropoff;
                nearbyObject.transform.GetComponent<Enemy>().TakeDamage(finalDamage);
                //Debug.Log(finalDamage);
            }
            Rigidbody rigidBody = nearbyObject.GetComponent<Rigidbody>();
            if (rigidBody != null){
                rigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraShake>().ShakeIncreaseDistance(25f, 1.5f, GameController.Instance.Player, gameObject);
        AudioController.Instance.Play_RandomPitch_InWorldspace("Explosion", gameObject, 0.95f, 1f);
        fire = Instantiate(fireEffect, transform.position, Quaternion.identity);
        Destroy(explosion, 3.75f);
        if (InstantiateFire) {
            Destroy(fire, 10f);
        }
    }
}
