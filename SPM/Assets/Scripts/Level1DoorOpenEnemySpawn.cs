using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1DoorOpenEnemySpawn : MonoBehaviour
{
    //Author: Teo
    public GameObject aDoor;
    public GameObject parent;
    public SpawnManager spawnManager;
    bool isBlack;
    Animator anim;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = parent.GetComponent<Renderer>();
        anim = aDoor.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("InteractionPlayer") && GameController.Instance.playerIsInteracting)
        {
            Debug.Log("F");
            spawnManager.InitializeSpawner();
            anim.SetBool("isOpen", true);
            isBlack = !isBlack;
            
            Destroy(gameObject);
            if (isBlack)
            {
                rend.material.color = Color.red;
            }
            else
            {
                rend.material.color = Color.black;
                

                //if(hasSpawner)
            }

        }
    }
}
