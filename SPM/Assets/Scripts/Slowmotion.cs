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
}
