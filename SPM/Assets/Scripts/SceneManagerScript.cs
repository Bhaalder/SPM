using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public ArenaButton aB;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    

    }

    // Update is called once per frame
    void Update()
    {
      
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && aB.ButtonPressed && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Level2WhiteBox");
        }
        else
        {
            Debug.Log("Du måste låsa upp dörren!!");
        }
    }
}
