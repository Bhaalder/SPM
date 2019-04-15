using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    private float projectileSpeed;
    private float projectileDamage;
    private float projectileForce;

    private void Update()
    {
        transform.position += transform.forward * projectileSpeed * 0.03f;
        IncreaseSpeed();
    }

    private void IncreaseSpeed()
    {
        if (projectileSpeed < 50)
        {
            projectileSpeed *= 1.2f;
        }
    }

    public void SetProjectileSpeed(float speed)
    {
        projectileSpeed = speed;
    }
    public void SetProjectileDamage(float damage) {
        projectileDamage = damage;
    }
    public void SetProjectileForce(float force) {
        projectileForce = force;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Explosion>().Explode(projectileForce, projectileDamage);
    }
}
