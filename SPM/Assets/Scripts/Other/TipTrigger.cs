using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TipTrigger : MonoBehaviour{
    //Author: Patrik Ahlgren

    [SerializeField] private string voiceLine;
    [SerializeField] private string voiceLineInterrupt;
    [TextArea(1,20)]
    [SerializeField] private string tipText;
    [SerializeField] private float waitBeforeFade = 5;
    [SerializeField] private bool hasVoiceLine;
    [SerializeField] private bool hasTip;
    [SerializeField] private bool kommandoSpecial;

    private bool isTriggered;
    private bool shouldBeOff = true;

    public GameObject thePrefab;
    public GameObject theCanvas;

    //GameObject[] canvasObjArr;
    GameObject[] arr;

    private void Start()
    {
        arr = new GameObject[2];
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("InteractionPlayer")) {
            if (!isTriggered) {
                if (!GameController.Instance.GetComponent<Timer>().TimerIsActive) {
                    GameController.Instance.GetComponent<Timer>().TimerIsActive = true;
                }
                if (hasVoiceLine) {            
                    AudioController.Instance.Stop(voiceLineInterrupt);
                    AudioController.Instance.PlayVoiceLine(voiceLine);
                }
                if (hasTip) {

                    //TutorialController.Instance.tutorialCanvasObject.SetActive(true);
                    //TutorialController.Instance.tutorialCanvasObject.SetActive(true);
                    TutorialController.Instance.TipText.text = tipText;
                    float voiceLineLength = AudioController.Instance.GetSoundLength(voiceLine);
                    //StartCoroutine(FadeText(voiceLineLength + waitBeforeFade, 5, TutorialController.Instance.TipText));

                    //canvasObjArr = GameObject.FindGameObjectsWithTag("IntegratedTutorial");

                    if (arr.Length > 1)
                    {
                        //ArrayCleanUp();
                        Destroy(arr[0]);
                    }

                    
                    
                    


                   

                   

                    Debug.Log(1);
                    GameObject instanceObject = Instantiate(thePrefab, theCanvas.transform);
                    instanceObject.GetComponentInChildren<TextMeshProUGUI>().text = tipText;
                    arr[0] = instanceObject;

                    
                }
                isTriggered = true;
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("InteractionPlayer"))
    //    {
           
    //        if (!isTriggered)
    //        {
                
    //        }

    //        //TutorialController.Instance.tutorialCanvasObject.SetActive(true);

    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractionPlayer"))
        {
            Debug.Log(2);
            isTriggered = false;
            //TutorialController.Instance.tutorialCanvasObject.SetActive(false);
            if (kommandoSpecial)
            {
                RemoveTutorial(3, arr[0]);
            }
            else
            {
                //ArrayCleanUp();
                Destroy(arr[0]);
                Destroy(gameObject);
            }

           
        }
    }

    //public IEnumerator FadeText(float waitBeforeFade, float fadeTime, TextMeshProUGUI tipText) {     
    //    tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, 1);
    //    yield return new WaitForSeconds(waitBeforeFade);
    //    while (tipText.color.a > 0.0f) {
    //        tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, tipText.color.a - (Time.unscaledDeltaTime / fadeTime));
    //        yield return null;
    //    }
    //    Destroy(gameObject);
    //}

    public IEnumerator RemoveTutorial(float waitBeforeRemove, GameObject iTutorial)
    {

        yield return new WaitForSeconds(waitBeforeRemove);


        Destroy(iTutorial);
        Destroy(gameObject);
    }

    private void ArrayCleanUp()
    {
        
        for (int i = 0; i <= arr.Length; i++)
        {
            
            Debug.Log(3);
            Destroy(arr[i]);
        }

        //if(canvasObjArr.Length > 0)
        //{
        //    Destroy(canvasObjArr[0]);

        //}
    }


}
