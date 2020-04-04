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

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (!isTriggered) {
                if (!GameController.Instance.GetComponent<Timer>().TimerIsActive) {
                    GameController.Instance.GetComponent<Timer>().TimerIsActive = true;
                }
                if (hasVoiceLine) {            
                    AudioController.Instance.Stop(voiceLineInterrupt);
                    AudioController.Instance.PlayVoiceLine(voiceLine);
                }
                if (hasTip) {
                    TutorialController.Instance.tutorialCanvasObject.SetActive(true);
                    TutorialController.Instance.TipText.text = tipText;
                    float voiceLineLength = AudioController.Instance.GetSoundLength(voiceLine);
                    //StartCoroutine(FadeText(voiceLineLength + waitBeforeFade, 5, TutorialController.Instance.TipText));
                    StartCoroutine(RemoveTutorial(waitBeforeFade, TutorialController.Instance.tutorialCanvasObject));

                }
                isTriggered = true;
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


        iTutorial.SetActive(false);
    }


}
