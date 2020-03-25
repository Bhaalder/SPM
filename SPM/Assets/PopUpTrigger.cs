using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{
    [SerializeField] private string SubjectText;
    [SerializeField] private string InfoText;

    bool isTriggered;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractionPlayer")){

            if (!isTriggered)
            {
                GameController.Instance.PauseAudio = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameController.Instance.PopUp.SetActive(true);

                GameController.Instance.popUpTextSubject.text = SubjectText;
                GameController.Instance.popUpTextInfo.text = InfoText;
                GameController.Instance.GamePaused();
                isTriggered = true;
            }
                
                
            

            

            


        }
        

    }
    
    
}
