using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipTrigger : MonoBehaviour{

    [SerializeField] private string voiceLine;
    [SerializeField] private string tipText;
    [SerializeField] private bool hasVoiceLine;
    [SerializeField] private bool hasTip;

    private bool isTriggered;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (!isTriggered) {
                if (hasVoiceLine) {
                    //AudioController.Instance.PlayVoiceLine(voiceLine);
                    AudioController.Instance.Play(voiceLine);
                }
                if (hasTip) {
                    GameController.Instance.TipText.text = tipText;
                    StartCoroutine(FadeText(AudioController.Instance.GetSoundLength(voiceLine) + 5, 5, GameController.Instance.TipText));
                }
                isTriggered = true;
            }
        }
    }

    public IEnumerator FadeText(float waitBeforeFade, float fadeTime, Text tipText) {     
        tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, 1);
        yield return new WaitForSeconds(waitBeforeFade);
        while (tipText.color.a > 0.0f) {
            tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, tipText.color.a - (Time.unscaledDeltaTime / fadeTime));
            yield return null;
        }
        Destroy(gameObject);
    }
}
