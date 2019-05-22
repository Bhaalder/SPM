//Marcus Söderberg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ProjectileAttackState")]
public class ProjectileAttackState : EnemyBaseState
{
    // Attributes
    [SerializeField] private float chaseDistance;

    private ProjectileWeapon enemyWeapon;
    private float cooldown;

    private float currentCool;

    // Methods
    public override void Enter()
    {
        base.Enter();
        enemyWeapon = WeaponController.Instance.GetEnemyProjectileWeapon();

        cooldown = enemyWeapon.GetFireRate();
    }

    public override void HandleUpdate()
    {
        Attack();

        if (!CanSeePlayer())
            owner.Transition<ProjectileChaseState>();
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > chaseDistance)
            owner.Transition<ProjectileChaseState>();
    }

    private void Attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;

        GameObject enemyProj = Instantiate(enemyWeapon.GetProjectile(), owner.transform.position + owner.transform.forward * 2, Quaternion.identity);
        enemyProj.GetComponent<EnemyProjectile>().SetProjectileSpeed(enemyWeapon.GetProjectileSpeed());
        enemyProj.GetComponent<EnemyProjectile>().SetProjectileTravelDistance(enemyWeapon.GetRange());
        enemyProj.GetComponent<EnemyProjectile>().SetProjectileDamage(enemyWeapon.GetDamage());

        currentCool = cooldown;
    }
}
