//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileEnemy : Enemy
{
    public override void TakeDamage(float damage)
    {

        health = health - (damage - damageResistance);
        if (health <= 0)
        {
            Death();
        }
        if (isDamaged == false)
        {
            Transition<ProjectileChaseState>();
            isDamaged = true;
        }
    }
}
