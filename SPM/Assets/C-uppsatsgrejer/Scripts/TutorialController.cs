using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{

    public Text TipText { get; set; }

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

            TipText = GameObject.Find("TipText").GetComponent<Text>();
            TipText.color = new Color(TipText.color.r, TipText.color.g, TipText.color.b, 0);
        


        PopUp.SetActive(false);

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
        TipText.text = s1;

        StartCoroutine(FadeText(0 + 0, 5, TipText));

        return s1;
    }
    public IEnumerator FadeText(float waitBeforeFade, float fadeTime, Text tipText)
    {

        tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, 1);

        yield return new WaitForSeconds(waitBeforeFade);
        while (tipText.color.a > 0.0f)
        {
            tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, tipText.color.a - (Time.unscaledDeltaTime / fadeTime));
            yield return null;
        }


    }
}
