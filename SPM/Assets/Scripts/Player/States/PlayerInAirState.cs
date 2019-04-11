using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateHandler;
using System;

public class PlayerInAirState : PlayerState<PlayerMovement>
{
    private static PlayerInAirState instance;

    private PlayerInAirState()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }

    public static PlayerInAirState Instance
    {
        get
        {
            if(instance == null)
            {
                new PlayerInAirState();
            }

            return instance;
        }
    }

    private void Update()
    {

    }

    public override void EnterState(PlayerMovement owner)
    {
        Debug.Log("Enter air state");
    }

    public override void ExitState(PlayerMovement owner)
    {

    }

    public override void UpdateState(PlayerMovement owner)
    {
        owner.ApplyGravity();
        owner.Run();

        if (owner.GetPlayerColl().GetGroundCollision())
        {
            owner.playerStateMachine.ChangeState(PlayerOnGroundState.Instance);
        }
    }
}
