// Daniel Fors
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/MeleeChaseState")]
public class MeleeChaseState : EnemyBaseState
{
    // Attributes
    [SerializeField] private float attackDistance;
    [SerializeField] private float lostTargetDistance;

    // Methods
    public override void HandleUpdate()
    {
        owner.agent.SetDestination(owner.player.transform.position);

        if (!CanSeePlayer())
            owner.Transition<MeleeAlertState>();
        else if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < attackDistance)
            owner.Transition<MeleeAttackState>();
    }
}
