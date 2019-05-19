using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsAnimated : MonoBehaviour
{
    Renderer rend;
    bool isBlack;
    bool isOpen;
    bool isClosed;

    
    public Animator anim;
    //public AnimationClip Close;
    //public AnimationClip Open;
    // Start is called before the first frame update
    void Start()
    {
        //green.SetActive(false);
        //red.SetActive(true);
        rend = GetComponent<Renderer>();
        rend.material.color = Color.black;
        
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
                
            }
            else
            {


                //anim.Play("SlidingDoorsClose");
                anim.SetBool("isOpen", false);
                
            }
            
            isBlack = !isBlack;
            //green.SetActive(!green.activeSelf);
            //red.SetActive(!red.activeSelf);

            if (isBlack)
            {
                rend.material.color = Color.red;
            }
            else
            {
                rend.material.color = Color.black;




            }

        }
    }
}

