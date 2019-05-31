﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    private bool canDamage;
    // Start is called before the first frame update
    void Start()
    {
        canDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current Animator State is: " + GetComponentInParent<Enemy>().CurrentAnimatorState("MeleeAttack"));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GetComponentInParent<Enemy>().CurrentAnimatorState("MeleeAttack") == true && canDamage == true)
        {
            Debug.Log("Animation MeleeAttack is running");
            GetComponentInParent<Enemy>().DoMeleeDamage();
            StartCoroutine(TimeBetweeenDamage());
        }
    }

    private IEnumerator TimeBetweeenDamage()
    {
        canDamage = false;
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }
}
