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
    public float EnterMoveSeconds = 0.5f;

    public int enemyType;

    public Transform gameController;
    public Transform spawnPoint;
    public Transform player;

    public bool AttackTrigger;
    public bool Attacking;
    public bool Charging;
    public bool RecentlyCharged;
    public bool timerRunning = true;



    private Vector3 chargeposition;

    private GameObject objSpawn;
    private int SpawnerID;
    private int i;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        AttackTrigger = true;
        Attacking = false;
        Charging = false;
        RecentlyCharged = false;
        objSpawn = (GameObject)GameObject.FindWithTag("Spawner");

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        JustEntered();

    }

    public void TakeDamage(float damage){
		health = health - (damage - DamageResistance);
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
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
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
        if (!Attacking)
        {
            if (Vector3.Distance(transform.position, player.position) >= MinDist && Vector3.Distance(transform.position, player.position) <= MaxDist)
            {
                transform.LookAt(player);


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
        Debug.Log("damage taken");
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
        if (collision.gameObject.tag == "Player" && RecentlyCharged) {
            Debug.Log("just collided with player");
            gameController.GetComponent<GameController>().TakeDamage((int)damage*3);
        }
    }

    // Sets bool on respawn point
    void OnDeathRespawn()
    {

        GetComponentInParent<SpawnManager>().EnemyDefeated();
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
