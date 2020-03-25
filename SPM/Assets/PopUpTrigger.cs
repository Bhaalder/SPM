using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{
    [SerializeField] private string SubjectText;
    [SerializeField] private string InfoText;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            GameController.Instance.GamePaused();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameController.Instance.PopUp.SetActive(true);
            
            GameController.Instance.popUpTextSubject.text = SubjectText;
            GameController.Instance.popUpTextInfo.text = InfoText;
            
            
        }
        

    }
}
