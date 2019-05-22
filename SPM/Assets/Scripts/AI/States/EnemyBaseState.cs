// Daniel Fors
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : State
{
    // Attributes
    [SerializeField] protected float moveSpeed;
    protected Enemy owner;

    // Methods
    public override void Enter()
    {
        owner.agent.speed = moveSpeed;
    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (Enemy)owner;
    }

    protected bool CanSeePlayer()
    {
        return !Physics.Linecast(owner.transform.position, owner.player.transform.position, owner.visionMask);
    }
}
