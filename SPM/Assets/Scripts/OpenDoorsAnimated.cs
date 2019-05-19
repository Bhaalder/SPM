using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsAnimated : MonoBehaviour
{
    
    
    bool isOpen;
    bool isClosed;

    public GameObject redP;
    public GameObject greenP;

    
    public Animator anim;
    //public AnimationClip Close;
    //public AnimationClip Open;
    // Start is called before the first frame update
    void Start()
    {
        greenP.SetActive(false);
        redP.SetActive(true);
        
        

        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("InteractionPlayer") && GameController.Instance.playerIsInteracting)
        {

            isOpen = !isOpen;
            Debug.Log("F");
            if (isOpen)
            {
                //anim.Play("SlidingDoorsOpen");
                anim.SetBool("isOpen", true);
                greenP.SetActive(!greenP.activeSelf);
                redP.SetActive(!redP.activeSelf);

            }
            else
            {


                //anim.Play("SlidingDoorsClose");
                anim.SetBool("isOpen", false);
                greenP.SetActive(!greenP.activeSelf);
                redP.SetActive(!redP.activeSelf);



            }






        }
    }
}

