//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{

    // Methods
    public override void TakeDamage(float damage)
    {

        health = health - (damage - damageResistance);
        if (health <= 0)
        {
            Death();
        }
        Transition<MeleeAlertState>();
    }
}
