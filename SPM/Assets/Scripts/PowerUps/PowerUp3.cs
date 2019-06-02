using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp3 : MonoBehaviour
{
    //Author: Marcus Söderberg
    private Animator anim;

    private void Start()
    {
        anim = GameObject.Find("SliderArmor").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractionPlayer")
        {
            if(GameController.Instance.PlayerArmor < 100)
            {
                GameController.Instance.GetComponent<GameController>().PlayerArmor = 100;
                GetComponentInParent<PowerUpSpawner>().Respawner();
                AudioController.Instance.Play("HPShieldPickup");
                Destroy(gameObject);
                anim.SetTrigger("FullArmor");
            }
        }
    }

}
