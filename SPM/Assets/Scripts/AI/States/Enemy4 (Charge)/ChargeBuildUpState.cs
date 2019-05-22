//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ChargeBuildUpState")]
public class ChargeBuildUpState : EnemyBaseState
{
    [SerializeField] private float buildUpTime;
    private float currentCool;


    public override void Enter()
    {
        base.Enter();
        currentCool = buildUpTime;

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
