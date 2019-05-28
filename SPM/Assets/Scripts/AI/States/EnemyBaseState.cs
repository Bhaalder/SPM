﻿// Daniel Fors
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : State
{
    // Attributes
    [SerializeField] protected float moveSpeed;
    protected Enemy owner;
    private float distanceToPlayer;

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
        bool lineHit = Physics.Linecast(owner.transform.position, owner.player.transform.position, out RaycastHit hit, owner.visionMask);
        Debug.DrawLine(owner.transform.position, hit.point, Color.red);
        return !lineHit;
    }

    protected float DistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(owner.transform.position, owner.player.transform.position);
        return distanceToPlayer;
    }

}
