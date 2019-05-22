//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/MeleeAttackState")]
public class MeleeAttackState : EnemyBaseState
{
    // Attributes
    [SerializeField] private float chaseDistance;
    [SerializeField] private float minDistance;
    [SerializeField] private float cooldown;
    [SerializeField] private float damage;


    private float currentCool;

    // Methods
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > minDistance)
        {
            owner.agent.SetDestination(owner.player.transform.position);
        }
        Attack();

        if (!CanSeePlayer())
            owner.Transition<MeleeAlertState>();
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > chaseDistance)
            owner.Transition<MeleeChaseState>();
    }

    private void Attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;

        GameController.Instance.TakeDamage((int)damage);

        currentCool = cooldown;
    }
}
