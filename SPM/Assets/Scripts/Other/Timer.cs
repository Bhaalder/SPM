using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{

    private float minuteTimeCount;
    private float secondsTimeCount;
    private float totalSecondsTimeCount;

    public bool TimerIsActive;

    private Text timerText;

    private void Start() {
       timerText = GameObject.Find("TimerText").GetComponent<Text>();
        timerText.text = "00:00.00";
        secondsTimeCount = 30;//TA BORT SEN
        minuteTimeCount = 28; // TA BORT SEN
    }

    private void Update() {
        if (TimerIsActive) {
            if (!GameController.Instance.GameIsPaused) {
                secondsTimeCount += Time.unscaledDeltaTime;
            }
            if (secondsTimeCount >= 60f) {
                secondsTimeCount -= 60;
                ++minuteTimeCount;
            }
            timerText.text = minuteTimeCount.ToString("00") + ":" + secondsTimeCount.ToString("00.00");
            if (secondsTimeCount < 0) {//ifall vi ska ha system att tiden minskar om man dödar fiender eller något
                secondsTimeCount = 0;
            }
            totalSecondsTimeCount = (minuteTimeCount * 60) + secondsTimeCount;
        }       
    }

    public GameObject GetTimerObject() {
        return gameObject;
    }

    public float GetFinalTime() {
        return totalSecondsTimeCount;
    }

    public void SetTimer(float timer) {
        secondsTimeCount = timer;
    }

    public void AddToTimer(float timeAdded) {
        secondsTimeCount += timeAdded;
    }

    public void SubtractFromTimer(float timeSubtracted) {
        secondsTimeCount -= timeSubtracted;
    }
}
