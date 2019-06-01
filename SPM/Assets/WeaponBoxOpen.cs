using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoxOpen : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isOpening", true);
            AudioController.Instance.Play_InWorldspace("Box", gameObject);
            Collider col = GetComponent<Collider>();
            col.enabled = false;
        }
    }

}
