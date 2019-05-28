//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ChargeStunnedState")]
public class ChargeStunnedState : EnemyBaseState
{
    // Attributes
    [Tooltip("Time in Seconds the Enemy is stunned for.")]
    [SerializeField] private float stunnedForSeconds;
    [Tooltip("Distance at which the tries to attack with Melee Attack")]
    [SerializeField] private float meleeAttackDistance;

    private float currentCool;

    // Methods
    public override void Enter()
    {
        base.Enter();
        currentCool = stunnedForSeconds;
        //Animation Idle
    }

    public override void HandleUpdate()
    {
        Stunned();
    }

    void Stunned()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
        {
            return;
        }

        currentCool = stunnedForSeconds;
        owner.DealtDamage = false;
        if (DistanceToPlayer() > meleeAttackDistance)
        {
            owner.Transition<ChargeChaseState>();
        }
        if (DistanceToPlayer() <= meleeAttackDistance)
        {
            owner.Transition<ChargeMeleeAttackState>();
        }

    }

}
