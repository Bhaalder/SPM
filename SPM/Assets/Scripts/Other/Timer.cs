using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerText;
    double time;

    private void Start() {
       timerText = GameObject.Find("TimerText").GetComponent<Text>();
    }

    private void Update() {
        timerText.text = time.ToString("F2");
        if (!GameController.Instance.GameIsPaused) {
            time += Time.unscaledDeltaTime;
        }       
    }

}
