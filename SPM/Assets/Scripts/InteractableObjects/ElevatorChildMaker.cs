using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorChildMaker : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
            Debug.Log("spelaren child");
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;

        }
    }
}
