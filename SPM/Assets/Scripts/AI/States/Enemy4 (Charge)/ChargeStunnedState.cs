//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ChargeStunnedState")]
public class ChargeStunnedState : EnemyBaseState
{
    // Attributes
    [SerializeField] private float stunnedForSeconds;
    private float currentCool;

    // Methods
    public override void Enter()
    {
        base.Enter();
        currentCool = stunnedForSeconds;
    }

    public override void HandleUpdate()
    {
        Stunned();
    }

    void Stunned()
    {
        Debug.Log("I am stunned");

        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;
        currentCool = stunnedForSeconds;
        owner.setDealtDamage(false);
        Debug.Log("I am no longer stunned");

        owner.Transition<ChargeChaseState>();
    }

}
