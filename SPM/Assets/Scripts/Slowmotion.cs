using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowmotion:MonoBehaviour{
    public float slowdownAmount;
    
    public void SlowTime() {
        if (!GameController.Instance.gameIsSlowmotion) {
            Time.timeScale = slowdownAmount;
        } else if (GameController.Instance.gameIsSlowmotion) {
            Time.timeScale = 1f;
        }
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        GameController.Instance.gameIsSlowmotion = !GameController.Instance.gameIsSlowmotion;
    }

    private void Update() {
        if (GameController.Instance.gameIsSlowmotion && GameController.Instance.SlowmotionSlider.value == 0) {
            GameController.Instance.gameIsSlowmotion = false;
            Time.timeScale = 1f;
        }
        if (!GameController.Instance.gameIsSlowmotion && GameController.Instance.SlowmotionSlider.value < 100) {
            GameController.Instance.SlowmotionSlider.value += 5 * Time.deltaTime; //20sek
        } else if (GameController.Instance.gameIsSlowmotion) {
            GameController.Instance.SlowmotionSlider.value -= 100 * Time.deltaTime; //10sek
        }
    }

}
