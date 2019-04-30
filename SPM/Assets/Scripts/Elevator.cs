using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject triggerGO;
    [SerializeField] private Transform platformTrans;
    [SerializeField] private Transform lowerPointTrans;
    [SerializeField] private Transform higherPointTrans;
    [SerializeField] private bool hasSwitch;
    private bool moving = false;
    private bool movingUp = true;
    [SerializeField] private float movementSpeed;

    
    //spelaren så den kan bli child
    
    private void Start()
    {
        // sätter att knappen ska bli child så den kan följa med hissen
        
    }

    private void Update()
    {
        ControlTrigger();
        if (moving)
        {
            if (movingUp)
                MoveUp();
            else
                MoveDown();
        }
    }

    public bool GetMoving()
    {
        return moving;
    }

    public Vector3 GetMovementSpeed()
    {
        return new Vector3(0, movementSpeed * 0.1f, 0);
    }

    private void ControlTrigger()
    {
        if (triggerGO.GetComponent<Trigger>().GetTriggered() && !hasSwitch)
            ToggleMove();
        else if (triggerGO.GetComponent<Trigger>().GetTriggered() && hasSwitch && Input.GetKeyDown(KeyCode.E))       
            ToggleMove();      
    }

    private void ToggleMove()
    {
        if (moving)
            moving = false;
        else
            moving = true;
    }

    private void MoveUp()
    {
        if (platformTrans.position.y < higherPointTrans.position.y)
            platformTrans.position += new Vector3(0, movementSpeed * 0.05f, 0);
        else
        {
            movementSpeed *= -1;
            moving = false;
            movingUp = false;
        }
    }

    private void MoveDown()
    {
         if (platformTrans.position.y > lowerPointTrans.position.y)
            platformTrans.position += new Vector3(0, movementSpeed * 0.05f, 0);
        else
        {
            movementSpeed *= -1;
            moving = false;
            movingUp = true;
        }
    }

    


    
    


}
