using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject projectile;

	public float health = 20;
    public float damage = 5;
    public float MoveSpeed = 4;
    public float MaxDist = 10;
    public float MinDist = 0.1f;
    public float AttackDistance = 5;
    public float DamageResistance = 5;
    public float EnterMoveSeconds = 0.5f;
    public float TimeBeforeCharge = 2f;
    public float EnemyStunnedTime = 3f;
    public float TimeBetweenAttacks = 0.5f;
    public float projectileSpeed;
    public float projectileDamage;
    public float ProjectileTravelDistance;

    public int enemyType;

    public Transform gameController;
    public Transform spawnPoint;
    public Transform player;

    public bool AttackTrigger;
    public bool Attacking;
    public bool BeingAttacked;
    public bool Charging;
    public bool RecentlyCharged;
    public bool timerRunning = true;
    public bool isStunned;
    


    private Vector3 chargeposition;
    private Vector3 noChargePosition;

    private GameObject objSpawn;
    private int SpawnerID;
    private int i;

    private bool activated;

    private ProjectileWeapon enemyWeapon;
    // Start is called before the first frame update
    void Start(){
        enemyWeapon = WeaponController.Instance.GetEnemyProjectileWeapon();
        player = GameObject.Find("InteractionPlayer").transform;
        AttackTrigger = true;
        Attacking = false;
        Charging = false;
        RecentlyCharged = false;
        BeingAttacked = false;
        isStunned = false;
        activated = false;
        objSpawn = (GameObject)GameObject.FindWithTag("Spawner");
        noChargePosition = new Vector3(0, 0, 0);
        chargeposition = noChargePosition;

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        JustEntered();
        if (activated && !Charging)
        {
            LookAtPlayer();
        }
        transform.rotation.Normalize();

    }

    public void TakeDamage(float damage){
		health = health - (damage - DamageResistance);
        BeingAttacked = true;
        if (health <= 0){
            OnDeathRespawn();
            //removeMe();
            Destroy(gameObject);
        }
	}

    void JustEntered()
    {
        if (timerRunning)
        {
            EnterMoveSeconds -= Time.smoothDeltaTime;
            if (EnterMoveSeconds >= 0)
            {
                //transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.position, MoveSpeed * Time.deltaTime);
            }
            else
            {
                Debug.Log("Done");
                timerRunning = false;
            }
        }
    }

    
    public void Movement()
    {

        if (!isStunned)
        {
            if (!Attacking)
            {
                if (Vector3.Distance(transform.position, player.position) >= MinDist && Vector3.Distance(transform.position, player.position) <= MaxDist)
                {
                    activated = true;
                    MoveToPlayer();
                }
                if (Vector3.Distance(transform.position, player.position) <= AttackDistance && !Attacking)
                {
                    Attacking = true;
                    Attack();
                }
                if (BeingAttacked)
                {
                    MoveToPlayer();
                }

            }
            else
            {
                Attack();
            }
        }
    }

    private void MoveToPlayer()
    {

        LookAtPlayer();
        if (Vector3.Distance(transform.position, player.position) > AttackDistance)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }

    }

    // Attacking actions
    void Attack() {
        if (AttackTrigger) {

            switch (enemyType) {
                case 5:
                    //add attacking move for enemy 5
                    break;
                case 4:
                    ChargeAttack();
                    break;
                case 3:
                    RangeAttack();
                    break;
                case 2:
                    //add attacking move for enemy 2
                    break;
                case 1:
                    MeleeAttack();
                    break;

            }

        }
    }

    void MeleeAttack() {
        GameController.Instance.TakeDamage((int)damage);
        AttackTrigger = false;
        Attacking = false;
        StartCoroutine(AttackTimer(TimeBetweenAttacks));
    }
    void ChargeAttack() {
        if (RecentlyCharged)
        {
            if (!Charging)
            {
                Debug.Log(chargeposition);
                float step = (MoveSpeed*5) * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, chargeposition, step);
                if (transform.position == chargeposition) { StunEnemy(); chargeposition.Set(0,0,0); }
            }
            else
            {

            }
        }
        else {
            if (chargeposition == noChargePosition)
            {
                chargeposition = player.position;
                RecentlyCharged = true;
                StartCoroutine(ChargingTimer(TimeBeforeCharge));
            }

        }

    }

    void RangeAttack()
    {
        GameObject enemyProj = Instantiate(enemyWeapon.GetProjectile(), transform.position + transform.forward * 2, Quaternion.identity);
        enemyProj.GetComponent<EnemyProjectile>().SetProjectileSpeed(enemyWeapon.GetProjectileSpeed());  
        enemyProj.GetComponent<EnemyProjectile>().SetProjectileTravelDistance(enemyWeapon.GetRange());
        enemyProj.GetComponent<EnemyProjectile>().SetProjectileDamage(enemyWeapon.GetDamage());
        AttackTrigger = false;
        Attacking = false;
        StartCoroutine(AttackTimer(enemyWeapon.GetFireRate()));
        StunEnemy();
    }

    IEnumerator AttackTimer(float timeBetweenAttacks)
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        AttackTrigger = true;
    }

    IEnumerator ChargingTimer(float waitTime) {
        Charging = true;
        yield return new WaitForSeconds(waitTime);
        Charging = false;
    }

    IEnumerator StunTime(float EnemyStunnedTime)
    {
        isStunned = true;
        yield return new WaitForSeconds(EnemyStunnedTime);
        isStunned = false;
    }

    public void StunEnemy()
    {
        StartCoroutine(StunTime(EnemyStunnedTime));
        Attacking = false;
        RecentlyCharged = false;
        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        transform.LookAt(player, Vector3.up);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player" && RecentlyCharged) {
            GameController.Instance.TakeDamage((int)damage*3);
        }
    }

    // Sets bool on respawn point
    void OnDeathRespawn()
    {
        if (GetComponentInParent<SpawnManager>())
        {
            GetComponentInParent<SpawnManager>().EnemyDefeated();
        }
        //spawnPoint.GetComponent<EnemySpawnPoint>().spawnTrigger = true;
    }

    // Call this when you want to kill the enemy
    void removeMe()
    {
        objSpawn.BroadcastMessage("killEnemy", SpawnerID);
        Destroy(gameObject);
    }
    // this gets called in the beginning when it is created by the spawner script
    void setName(int sName)
    {
        SpawnerID = sName;
    }

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MaxDist);
    }
}
