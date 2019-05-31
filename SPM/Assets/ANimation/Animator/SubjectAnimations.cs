using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectAnimations : MonoBehaviour
{
    public Animator anim;

    public  bool isSuperHuman;
    public bool isAlien1;


    void Start()
    {
        if (isSuperHuman)
        {
            anim.SetBool("SuperHuman", true);
            AudioController.Instance.Play_InWorldspace_WithTag("LabBubbling", "SuperHuman");
        }else if (isAlien1)
        {
            anim.SetBool("Alien1", true);

        }
    }

    // Update is called once per frame
    
}
