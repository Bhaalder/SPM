//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ChargeAttackState")]
public class ChargeAttackState : EnemyBaseState
{
    // Attributes
    [SerializeField] private float chaseDistance;
    [SerializeField] private float minDistance;
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
