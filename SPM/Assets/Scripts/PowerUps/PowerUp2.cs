using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp2 : MonoBehaviour
{
    //Author: Marcus Söderberg
    public Transform powerUpSpawner;
    private Animator anim;
    //public float TimeToDestroy = 0.0f;

    private void Start()
    {
        anim = GameObject.Find("SliderHealth").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractionPlayer")
        {
            GameController.Instance.GetComponent<GameController>().PlayerHP = 100;
            GetComponentInParent<PowerUpSpawner>().Respawner();
            Destroy(gameObject);
            anim.SetTrigger("FullHealth");
            //StartCoroutine(UsedBoost());
        }
    }


    //IEnumerator UsedBoost()
    //{
    //    yield return new WaitForSeconds(TimeToDestroy);
    //    Destroy(gameObject);
    //}
}
