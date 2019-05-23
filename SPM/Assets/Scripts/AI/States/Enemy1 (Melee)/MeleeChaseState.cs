//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/MeleeChaseState")]
public class MeleeChaseState : EnemyBaseState
{
    // Attributes
    [SerializeField] private float attackDistance;

    // Methods
    public override void HandleUpdate()
    {
        owner.agent.SetDestination(owner.player.transform.position);

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < attackDistance)
        {
            Debug.Log("Inside of attack distance, starting attack!");
            owner.Transition<MeleeAttackState>();
        }
    }
}
