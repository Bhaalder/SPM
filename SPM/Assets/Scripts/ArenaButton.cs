using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaButton : MonoBehaviour
{
    public bool ButtonPressed;
    // Start is called before the first frame update
    void Start()
    {
        ButtonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            ButtonPressed = true;
            Debug.Log("Knappen tryckt");
        }
    }
}
