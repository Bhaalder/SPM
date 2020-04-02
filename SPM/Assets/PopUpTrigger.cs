﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrigger : MonoBehaviour
{
    [SerializeField] private string SubjectText;
    [SerializeField] private string InfoText;
    [SerializeField] bool isBoxTutorial;

    bool isTriggered;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractionPlayer")){

            if (!isTriggered)
            {

                PopUpMethod();
            }
                
                
            

            

            


        }
        

    }
    public void PopUpMethod()
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
    public void PopUpMethod(string subject, string info)
    {
        GameController.Instance.PauseAudio = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameController.Instance.PopUp.SetActive(true);

        GameController.Instance.popUpTextSubject.text = subject;
        GameController.Instance.popUpTextInfo.text = info;
        GameController.Instance.GamePaused();
        
    }
    
    
}