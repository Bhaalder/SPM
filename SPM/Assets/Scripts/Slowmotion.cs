using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowmotion:MonoBehaviour{
    //Author: Patrik Ahlgren

    public float slowdownAmount;
    
    public void SlowTime() {
        if (!GameController.Instance.gameIsPaused) {
            if (!GameController.Instance.gameIsSlowmotion) {               
                Time.timeScale = slowdownAmount;
                //AudioController.Instance.SFXSetPitch(0.5f);
            } else if (GameController.Instance.gameIsSlowmotion) {
                Time.timeScale = 1f;
                //AudioController.Instance.SFXSetPitch(1f);
            }
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            GameController.Instance.gameIsSlowmotion = !GameController.Instance.gameIsSlowmotion;
        }
    }

    private void Update() {
        if (GameController.Instance.gameIsSlowmotion && GameController.Instance.SlowmotionSlider.value == 0) {
            GameController.Instance.gameIsSlowmotion = false;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            //AudioController.Instance.SFXSetPitch(1f);
        }
        SlowmotionSlider();
    }

    private void SlowmotionSlider() {
        if (!GameController.Instance.gameIsPaused) {
            if (!GameController.Instance.gameIsSlowmotion && GameController.Instance.SlowmotionSlider.value < 100) {
                GameController.Instance.SlowmotionSlider.value += 5 * Time.unscaledDeltaTime; //20sek
            } else if (GameController.Instance.gameIsSlowmotion) {
                GameController.Instance.SlowmotionSlider.value -= 10 * Time.unscaledDeltaTime; //10sek
            }
        }
    }

}
