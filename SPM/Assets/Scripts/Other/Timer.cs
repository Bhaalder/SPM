using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{

    public double TimeCount;

    private Text timerText;

    private void Start() {
       timerText = GameObject.Find("TimerText").GetComponent<Text>();
    }

    private void Update() {
        timerText.text = TimeCount.ToString("F2");
        if (!GameController.Instance.GameIsPaused) {
            TimeCount += Time.unscaledDeltaTime;
        }       
    }

    public void AddToTimer(float timeAdded) {
        TimeCount += timeAdded;
    }
}
