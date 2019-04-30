using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsLvl1 : MonoBehaviour
{
   
    public GameObject theDoor;
    Renderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rend = GetComponent<Renderer>();
        rend.material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameController.Instance.playerIsInteracting)
        {
            theDoor.SetActive(!theDoor.activeSelf);
                rend.material.color = Color.red;

            
            
            
        }
       
    }

}
