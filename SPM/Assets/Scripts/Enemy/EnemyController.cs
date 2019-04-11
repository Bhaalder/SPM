using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	public float health = 20;
    public float damage = 5;
    public float MoveSpeed = 4;
    public float MaxDist = 10;
    public float MinDist = 0.1f;
    public float AttackDistance = 5;
    public float DamageResistance = 5;

    public int enemyType;

    public Transform gameController;
    public Transform spawnPoint;
    public Transform player;

    public bool AttackTrigger;
    public bool Attacking;
    public bool Charging;
    public bool RecentlyCharged;

    private Vector3 chargeposition;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find("Enemy_Spawn_Point").transform;
        player = GameObject.Find("Player").transform;
        AttackTrigger = true;
        Attacking = false;
        Charging = false;
        RecentlyCharged = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

	public void TakeDamage(float damage){
		health = health - (damage - DamageResistance);
        if (health <= 0){
            OnDeathRespawn();
            Destroy(gameObject); }
	}

    
    public void Movement()
    {
        if (!Attacking)
        {
            transform.LookAt(player);
            if (Vector3.Distance(transform.position, player.position) >= MinDist && Vector3.Distance(transform.position, player.position) <= MaxDist)
            {

                transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            }
            if (Vector3.Distance(transform.position, player.position) <= AttackDistance && !Attacking)
            {
                Attacking = true;
                Attack();
            }
        }
        else {
            Attack();
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
                    //add attacking move for enemy 3
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
        gameController.GetComponent<GameController>().TakeDamage((int)damage);
        AttackTrigger = false;
        Attacking = false;
        StartCoroutine(AttackTimer());
    }
    void ChargeAttack() {
        if (RecentlyCharged)
        {
            if (!Charging)
            {
                Debug.Log(chargeposition);
                float step = (MoveSpeed*5) * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, chargeposition, step);
            }
            else
            {

                //if (Vector3.Distance(transform.position, chargeposition) < MinDist) { RecentlyCharged = true; Attacking = false; }
            }
        }
        else {
            chargeposition = player.position;
            RecentlyCharged = true;
            StartCoroutine(ChargingTimer(2f));

        }


        //transform.position += chargeposition * (MoveSpeed*3) * Time.deltaTime;


        Debug.Log("move done");
        //Attacking = false;


    }

    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(0.5f);
        AttackTrigger = true;
    }

    IEnumerator ChargingTimer(float waitTime) {
        Charging = true;
        yield return new WaitForSeconds(waitTime);
        Charging = false;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("just collided with player");
            gameController.GetComponent<GameController>().TakeDamage((int)damage);
        }
    }

    // Sets bool on respawn point
    void OnDeathRespawn()
    {
        spawnPoint.GetComponent<EnemySpawnPoint>().spawnTrigger = true;
    }

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MaxDist);
    }
}
