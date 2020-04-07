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
    

    private bool isTriggered;
    private bool shouldBeOff = true;

    public GameObject thePrefab;
    public GameObject theCanvas;

    GameObject instanceObject;

    //GameObject[] canvasObjArr;
    //GameObject[] arr;

    private void Start()
    {
        //arr = new GameObject[2];
        
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

                    //if (arr.Length > 1)
                    //{
                    //    //ArrayCleanUp();
                    //    Destroy(arr[0]);
                    //}

                    if(instanceObject != null)
                    {
                        Destroy(instanceObject);
                    }
                    Debug.Log(1);
                    instanceObject = Instantiate(thePrefab, theCanvas.transform);
                    instanceObject.GetComponentInChildren<TextMeshProUGUI>().text = tipText;
                    //arr[0] = instanceObject;

                    
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

            Destroy(instanceObject);
            Destroy(gameObject);

            //TutorialController.Instance.tutorialCanvasObject.SetActive(false);
            //if (kommandoSpecial)
            //{
            //    Debug.Log(3);
            //    StartCoroutine(RemoveTutorial(instanceObject));

            //}
            //else
            //{
            //    //ArrayCleanUp();
            //    Destroy(instanceObject);
            //    Destroy(gameObject);
            //}
            //ArrayCleanUp();

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

    public IEnumerator RemoveTutorial(GameObject iTutorial)
    {
        Debug.Log(4);
        yield return new WaitForSeconds(3);

        Debug.Log(5);
        Destroy(iTutorial);
        Destroy(gameObject);
    }

    //private void ArrayCleanUp()
    //{
        
    //    for (int i = 0; i <= arr.Length; i++)
    //    {
            
    //        Debug.Log(3);
    //        Destroy(arr[i]);
    //    }

    //    //if(canvasObjArr.Length > 0)
    //    //{
    //    //    Destroy(canvasObjArr[0]);

    //    //}
    //}
    private void OnDestroy()
    {
        Debug.Log("Im ded");
    }

}
