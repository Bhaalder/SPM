﻿//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ChargeBuildUpState")]
public class ChargeBuildUpState : EnemyBaseState
{
    [Tooltip("Time in Seconds before the Enemy charges at the Player.")]
    [SerializeField] private float buildUpTime;
    private float currentCool;


    public override void Enter()
    {
        base.Enter();
        currentCool = buildUpTime;
        //Animation Idle
    }

    public override void HandleUpdate()
    {
        BuildUpTime();
    }

    void BuildUpTime()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;

        currentCool = buildUpTime;
        Debug.Log("I am done building up");

        owner.Transition<ChargeAttackState>();
        
    }

}
