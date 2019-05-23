//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/MeleeAttackState")]
public class MeleeAttackState : EnemyBaseState
{
    // Attributes
    [Tooltip("Distance at which the Enemy stops trying to attack and starts chasing the Player.")]
    [SerializeField] private float chaseDistance;
    [Tooltip("Time in Seconds between Attacks.")]
    [SerializeField] private float cooldown;
    [Tooltip("Damage done to Player with each Attack.")]
    [SerializeField] private int damage;


    private float currentCool;

    // Methods
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (owner.getIsDead() == false)
        {
            Attack();

            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > chaseDistance || CanSeePlayer() == false)
            {
                owner.Transition<MeleeChaseState>();
            }
        }
    }

    private void Attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
        {
            return;
        }

        if (CanSeePlayer() == true)
        {
            GameController.Instance.TakeDamage(damage);
        }

        currentCool = cooldown;
    }
}
