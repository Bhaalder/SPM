//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ProjectileIdleState")]
public class ProjectileIdleState : EnemyBaseState
{
    // Attributes
    [Tooltip("Distance at which the Enemy starts chasing the Player if it can see the Player.")]
    [SerializeField] private float chaseDistance;

    // Methods
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (CanSeePlayer() && Vector3.Distance(owner.transform.position, owner.player.transform.position) < chaseDistance)
        {
            owner.Transition<ProjectileChaseState>();
        }
    }
}
