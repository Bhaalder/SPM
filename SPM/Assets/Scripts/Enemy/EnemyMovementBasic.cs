using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBasic : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    public float MoveSpeed = 4;
	public float MaxDist = 10;
    public float MinDist = 5;
       

    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(Player);

		if (Vector3.Distance(transform.position, Player.position) >= MinDist && Vector3.Distance(transform.position, Player.position) <= MaxDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            
            if (Vector3.Distance(transform.position, Player.position) <= MinDist)
            {
                gameObject.GetComponent<EnemyController>().Attack();
            }

        }

    }
}
