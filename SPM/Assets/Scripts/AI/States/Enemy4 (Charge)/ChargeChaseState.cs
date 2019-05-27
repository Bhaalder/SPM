//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ChargeChaseState")]
public class ChargeChaseState : EnemyBaseState
{
    // Attributes
    [Tooltip("Distance at which Enemy will start Attacking the Player.")]
    [SerializeField] private float attackDistance;
    [Tooltip("Distance at which Enemy will stop chasing the Player if it can no longer see the Player.")]
    [SerializeField] private float stopChaseDistance;
    private bool isChasing;

    // Methods

    public override void Enter()
    {
        base.Enter();
        isChasing = true;
        //Animation Chase
        owner.animator.SetBool("isIdle", false);
        owner.animator.SetBool("isRunning", true);
        owner.animator.SetBool("isAttacking", false);
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

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < stopChaseDistance && CanSeePlayer() == false)
        {
            owner.Transition<ChargeIdleState>();
        }

    }
}
