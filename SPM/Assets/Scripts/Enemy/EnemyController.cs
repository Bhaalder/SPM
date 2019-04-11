using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	public float health = 20;
    public float damage = 5;
    public Transform gameController;
    public Transform spawnPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void TakeDamage(float damage){
		health = health - damage;
        if (health <= 0){
            GameObject.Find("Enemy_Spawn_Point").GetComponent<EnemySpawnPoint>().spawntrigger = true;
            Destroy(gameObject); }
	}

    public void Attack() {
        gameController.GetComponent<GameController>().TakeDamage((int)damage);
    }
}
