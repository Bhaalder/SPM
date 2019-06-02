using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp2 : MonoBehaviour
{
    //Author: Marcus Söderberg
    private Animator anim;

    private void Start()
    {
        anim = GameObject.Find("SliderHealth").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractionPlayer")
        {
            if (GameController.Instance.PlayerHP < 100)
            {
                GameController.Instance.GetComponent<GameController>().PlayerHP = 100;
                GetComponentInParent<PowerUpSpawner>().Respawner();
                AudioController.Instance.Play("HPShieldPickup");
                Destroy(gameObject);
                anim.SetTrigger("FullHealth");
            }
        }
    }
}
