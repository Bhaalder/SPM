using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{

    public TextMeshProUGUI TipText { get; set; }
    public GameObject tutorialCanvasObject { get; set; }

    ///NYTT FÖR TUTORIALS
    public GameObject PopUp;
    public TextMeshProUGUI popUpTextSubject { get; set; }
    public TextMeshProUGUI popUpTextInfo { get; set; }

    

    private GameObject cancelTutorialObject;

    public bool isTutorialTypePopUp;
    ///

    private static TutorialController _instance;

    public static TutorialController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TutorialController>();
#if UNITY_EDITOR
                if (FindObjectsOfType<TutorialController>().Length > 1)
                {
                    Debug.LogError("Found more than one gamecontroller");
                }
#endif
            }
            return _instance;
        }
    }


   
        
    void Start()
    {

        PopUp = GameObject.Find("PopUpTutorial");

        popUpTextSubject = GameObject.Find("SubjectText").GetComponent<TextMeshProUGUI>();
        popUpTextInfo = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();

        TipText = GameObject.Find("TipText").GetComponent<TextMeshProUGUI>();
        //TipText.color = new Color(TipText.color.r, TipText.color.g, TipText.color.b, 0);
        tutorialCanvasObject = GameObject.Find("TipTextObject");



        PopUp.SetActive(false);
        tutorialCanvasObject.SetActive(false);

        if (isTutorialTypePopUp)
        {
            cancelTutorialObject = GameObject.Find("IntegreradTutorials");
            cancelTutorialObject.SetActive(false);
        }
        else
        {
            cancelTutorialObject = GameObject.Find("PopUps");
            cancelTutorialObject.SetActive(false);
        }
    }
   

    public string TipMethod(string s1)
    {
        Debug.Log("method started");
        tutorialCanvasObject.SetActive(true);
        TipText.text = s1;

        StartCoroutine(RemoveTutorial(3,tutorialCanvasObject));

        return s1;
    }
    //public IEnumerator FadeText(float waitBeforeFade, float fadeTime, TextMeshProUGUI tipText)
    //{

    //    tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, 1);

    //    yield return new WaitForSeconds(waitBeforeFade);
    //    while (tipText.color.a > 0.0f)
    //    {
    //        tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, tipText.color.a - (Time.unscaledDeltaTime / fadeTime));
    //        yield return null;
    //    }


    //}
    public IEnumerator RemoveTutorial(float waitBeforeRemove, GameObject iTutorial)
    {

        yield return new WaitForSeconds(waitBeforeRemove);


        iTutorial.SetActive(false);
    }
}
