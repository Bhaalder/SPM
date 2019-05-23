// Daniel Fors
//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine
{
    // Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public NavMeshAgent agent;
    public LayerMask visionMask;
    public PlayerMovementController player;

    [SerializeField] protected float health;
    [SerializeField] protected float damageResistance;
    protected bool dealtDamage;
    protected bool isDamaged;

    private bool isDead;


    // Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        player = (PlayerMovementController)FindObjectOfType(typeof(PlayerMovementController));
        isDead = false;
        isDamaged = false;
        base.Awake();
    }

    public virtual void TakeDamage(float damage)
    {

        health = health - (damage - damageResistance);
        if (health <= 0)
        {
            Death();
        }
    }

    protected void Death()
    {
        if (GetComponentInParent<SpawnManager>() && !isDead)
        {
            isDead = true;
            GetComponentInParent<SpawnManager>().EnemyDefeated();
        }
        isDead = true;
        Destroy(gameObject);
    }

    public void setDealtDamage(bool a)
    {
        dealtDamage = a;
    }

    public bool getIsDead()
    {
        return isDead;
    }
}
