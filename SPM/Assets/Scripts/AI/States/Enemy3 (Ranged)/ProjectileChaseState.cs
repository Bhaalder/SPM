﻿//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ProjectileChaseState")]
public class ProjectileChaseState : EnemyBaseState
{
    // Attributes
    [SerializeField] private float attackDistance;

    // Methods
    public override void HandleUpdate()
    {
        owner.agent.SetDestination(owner.player.transform.position);

        if (!CanSeePlayer())
            owner.Transition<ProjectileAlertState>();
        else if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < attackDistance)
            owner.Transition<ProjectileAttackState>();
    }
}
