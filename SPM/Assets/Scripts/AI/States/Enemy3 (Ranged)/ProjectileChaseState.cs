//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ProjectileChaseState")]
public class ProjectileChaseState : EnemyBaseState
{
    // Attributes
    [SerializeField] private float attackDistance;

    // Methods
    public override void HandleUpdate()
    {
        try
        {
            owner.agent.SetDestination(owner.player.transform.position);
        }
        catch (Exception e){ Debug.Log("Set Destination Error: " + e); }

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < attackDistance)
        {
            owner.Transition<ProjectileAttackState>();
        }
    }
}
