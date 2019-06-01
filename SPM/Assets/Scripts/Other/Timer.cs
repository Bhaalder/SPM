using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{

    private float timeCount;

    private Text timerText;

    private void Start() {
       timerText = GameObject.Find("TimerText").GetComponent<Text>();
    }

    private void Update() {
        timerText.text = timeCount.ToString("F2");
        if (timeCount < 0) {//ifall vi ska ha system att tiden minskar om man dödar fiender eller något
            timeCount = 0;
        }
        if (!GameController.Instance.GameIsPaused) {
            timeCount += Time.unscaledDeltaTime;
        }       
    }

    public float GetTimer() {
        return timeCount;
    }

    public void SetTimer(float timer) {
        timeCount = timer;
    }

    public void AddToTimer(float timeAdded) {
        timeCount += timeAdded;
    }

    public void SubtractFromTimer(float timeSubtracted) {
        timeCount -= timeSubtracted;
    }
}
