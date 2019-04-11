using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	public float health = 20;
    public float damage = 5;
    public float MoveSpeed = 4;
    public float MaxDist = 10;
    public float MinDist = 3;
    public float AttackDistance = 5;
    public float DamageResistance = 5;
    public Transform gameController;
    public Transform spawnPoint;
    public Transform player;
    public bool AttackTrigger;



    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find("Enemy_Spawn_Point").transform;
        player = GameObject.Find("Player").transform;
        AttackTrigger = true;
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
        transform.LookAt(player);
        if (Vector3.Distance(transform.position, player.position) >= MinDist && Vector3.Distance(transform.position, player.position) <= MaxDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        }
        if (Vector3.Distance(transform.position, player.position) <= AttackDistance)
        {
            Attack();
        }
    }

    // Attacking actions
    void Attack() {
        if (AttackTrigger)
        {
            gameController.GetComponent<GameController>().TakeDamage((int)damage);
            AttackTrigger = false;
            StartCoroutine(AttackTimer());
        }
    }

    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(0.5f);
        AttackTrigger = true;
    }

    // Sets bool on respawn point
    void OnDeathRespawn()
    {
        spawnPoint.GetComponent<EnemySpawnPoint>().spawntrigger = true;
    }

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MaxDist);
    }
}
