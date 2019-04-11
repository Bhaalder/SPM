using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateHandler;

public class PlayerOnGroundState : PlayerState<PlayerMovement>
{
    private static PlayerOnGroundState instance;

    private PlayerOnGroundState()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static PlayerOnGroundState Instance
    {
        get
        {
            if (instance == null)
            {
                new PlayerOnGroundState();
            }

            return instance;
        }
    }

    public override void EnterState(PlayerMovement owner)
    {
        Debug.Log("Enter ground state");
    }

    public override void ExitState(PlayerMovement owner)
    {

    }

    public override void UpdateState(PlayerMovement owner)
    {
        owner.Run();

        if (Input.GetKeyDown(KeyCode.Space) && owner.GetPlayerColl().GetGroundCollision())
        {
            owner.Jump();
        }


        if (!owner.GetPlayerColl().GetGroundCollision())
        {
            owner.playerStateMachine.ChangeState(PlayerInAirState.Instance);
        }
    }
}
