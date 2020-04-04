using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopTutorial : MonoBehaviour
{
    [SerializeField]Button btn;
    [SerializeField] GameObject panel;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void Metod()
    {

        GameController.Instance.TutorialPaus();
        
        //panel.SetActive(false);
        gameObject.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
}
