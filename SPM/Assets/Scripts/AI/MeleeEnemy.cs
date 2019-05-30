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

        if (damage - damageResistance < 0)
        {

        }
        else
        {
            health -= (damage - damageResistance);
        }
        if (health <= 0)
        {
            Death();
        }
        if (isDamaged == false)
        {
            Transition<MeleeChaseState>();
            isDamaged = true;
        }
    }
}
