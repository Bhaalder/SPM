// Daniel Fors
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileEnemy : Enemy
{
    //Add some Projectile enemy specifics

    public override void TakeDamage(float damage)
    {

        health = health - (damage - damageResistance);
        if (health <= 0)
        {
            Death();
        }
        Transition<ProjectileAlertState>();
    }
}
