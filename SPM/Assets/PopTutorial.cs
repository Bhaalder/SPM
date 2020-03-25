using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopTutorial : MonoBehaviour
{
    [SerializeField]Button btn;
    [SerializeField] GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        //btn = transform.GetChild(0).Find("Button").GetComponent<Button>();
        //btn.onClick.AddListener(() => { Metod(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Metod()
    {

        GameController.Instance.GamePaused();
        //panel.SetActive(false);
        gameObject.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
}
