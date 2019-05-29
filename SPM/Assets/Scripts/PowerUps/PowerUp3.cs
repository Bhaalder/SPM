using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp3 : MonoBehaviour
{
    //Author: Marcus Söderberg
    public Transform powerUpSpawner;
    public Animator anim;
    //public float TimeToDestroy = 0.0f;

    private void Start()
    {
        anim = GameObject.Find("SliderArmor").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractionPlayer")
        {
            GameController.Instance.GetComponent<GameController>().PlayerArmor = 100;
            GetComponentInParent<PowerUpSpawner>().Respawner();
            Destroy(gameObject);//
            anim.SetTrigger("FullArmor");
            //StartCoroutine(UsedBoost());
        }
    }


    //IEnumerator UsedBoost()
    //{
    //    yield return new WaitForSeconds(TimeToDestroy);
    //    Destroy(gameObject);
    //}
}
