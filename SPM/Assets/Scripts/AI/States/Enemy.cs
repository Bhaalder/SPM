// Daniel Fors
//Marcus Söderberg
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine
{
    // Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public NavMeshAgent agent;
    public Animator animator;
    public LayerMask visionMask;
    public PlayerMovementController player;

    public bool HasRecentlyCharged { get; set; }

    public float health { get; set; }
    [SerializeField] protected float damageResistance;
    public int EnemyMeleeDamage { get; set; }
    public int ParentID { get; set; }

    public bool DealtDamage { get; set; }
    public bool CanDamage { get; set; }
    protected bool isDamaged;

    private bool isDead;
    private bool frozenRotation;
    private float unfreezeCooldown;

    // Methods
    protected override void Awake()
    {

        Renderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        player = (PlayerMovementController)FindObjectOfType(typeof(PlayerMovementController));
        isDead = false;
        isDamaged = false;
        CanDamage = false;
        frozenRotation = true;
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    public virtual void TakeDamage(float damage)
    {
        if (damage - damageResistance < 0)
        {

        }
        else
        {
            health -= (damage - damageResistance);
        }
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

    public bool getIsDead()
    {
        return isDead;
    }

    public void DoMeleeDamage()
    {
        GameController.Instance.TakeDamage(EnemyMeleeDamage);
    }

    public bool CurrentAnimatorState(string animatorTag)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag(animatorTag))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveEnemyData()
    {
        SaveSystem.SaveEnemyData(this);
    }
}
