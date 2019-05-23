//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ChargeAttackState")]
public class ChargeAttackState : EnemyBaseState
{
    // Attributes
    [Tooltip("Time in Seconds the charge will last for.")]
    [SerializeField] private float isChargingForSeconds;

    private float currentCool;
    private Vector3 chargePoint;

    // Methods
    public override void Enter()
    {
        base.Enter();
        chargePoint = owner.player.transform.position;
        currentCool = 0;
    }

    public override void HandleUpdate()
    {
        Attack();
    }

    private void Attack()
    {
        currentCool += Time.deltaTime;

        if (CanSeePlayer() == true)
        {
            if (owner.transform.position == chargePoint)
            {
                currentCool = 0;
                owner.Transition<ChargeStunnedState>();
            }

            if (currentCool < isChargingForSeconds)
            {
                owner.agent.SetDestination(chargePoint);
            }
            else
            {
                currentCool = 0;
                owner.Transition<ChargeStunnedState>();
            }
        }


    }
}
