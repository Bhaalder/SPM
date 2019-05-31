using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enemy4Roar()
    {
        AudioController.Instance.Play_InWorldspace("Enemy4Roar", gameObject);
    }
    public void Enemy4MeleeAttack()
    {
        AudioController.Instance.Play_InWorldspace("Enemy4MeleeAttack", gameObject);
    }
}
