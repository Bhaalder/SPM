//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ChargeChaseState")]
public class ChargeChaseState : EnemyBaseState
{
    // Attributes
    [SerializeField] private float attackDistance;
    private bool isChasing;

    // Methods

    public override void Enter()
    {
        base.Enter();
        isChasing = true;
    }
    public override void HandleUpdate()
    {

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < attackDistance && isChasing)
        {
            isChasing = false;
            
            owner.Transition<ChargeBuildUpState>();
            return;
        }
        owner.agent.SetDestination(owner.player.transform.position);


    }
}
