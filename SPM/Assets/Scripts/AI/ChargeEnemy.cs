//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeEnemy : Enemy
{
    [Tooltip("Damage done to Player if hit by the charge.")]
    [SerializeField] private int damage;

    protected override void Awake()
    {
        dealtDamage = false;
        base.Awake();
    }

    public override void TakeDamage(float damage)
    {

        health = health - (damage - damageResistance);
        if (health <= 0)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            Death();
        }
        if (isDamaged == false)
        {
            Transition<ChargeChaseState>();
            isDamaged = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !dealtDamage)
        {
            GameController.Instance.TakeDamage(damage);
            dealtDamage = true;
            Transition<ChargeStunnedState>();
        }
    }

}
