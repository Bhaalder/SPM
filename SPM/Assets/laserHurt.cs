using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserHurt : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.TakeDamage(400);
        }
    }
}
