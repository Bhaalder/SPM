using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsLvl1 : MonoBehaviour
{
    //Main author: Teo
    //Secondary author: Patrik Ahlgren

    public GameObject theDoor;

    //public GameObject green;
    //public GameObject red;

    Renderer rend;
    bool isBlack;
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
            Debug.Log("F");
            theDoor.SetActive(!theDoor.activeSelf);
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
