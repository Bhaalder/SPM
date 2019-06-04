using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameButton : MonoBehaviour {
    //Author: Patrik Ahlgren

    [SerializeField] private GameObject timerGO;
    private Timer timer;

    private ScoreScreen scoreScreen;

    private void Start() {
        timerGO = GameController.Instance.GetComponent<Timer>().GetTimerObject();
        timer = timerGO.GetComponent<Timer>();
        scoreScreen = GetComponent<ScoreScreen>();
    }

    public void PressButton() {
        timer.TimerIsActive = false;
        timerGO.transform.SetAsLastSibling();

    }

    private IEnumerator ExplosionEnding() {
        AudioController.Instance.Play_Delay("Explosion", 0.3f, 2.7f);
        Camera.main.GetComponent<CameraShake>().ShakeIncrease(25f, 1.5f);
        yield return new WaitForSeconds(1f);
        //skärmen fadear
        Camera.main.GetComponent<CameraShake>().ShakeIncrease(30f, 3f);
        yield return new WaitForSeconds(2f);
        Camera.main.GetComponent<CameraShake>().ShakeIncrease(15f, 1f);
        AudioController.Instance.FadeOut("Explosion", 5, 0);
        AudioController.Instance.StopAllSounds();
        scoreScreen.IsEndScreen = true;
        scoreScreen.CountScore(timer.GetFinalTime(), 10);//10 ska vara killcount
        yield return null;
    }

//    ENDGAME

//Dörren öppnas till sista utrymmet
//AudioController drar igång AlarmLjud vid knappen
//Tip: Press the button to save humanity!

//Knappen går att trycka på/dyker upp(tydlig placering)

//Klicka på knappen
//Timern stannar(set as LastSibling)
//Skärmen fadear
//Skärmen skakar
//Explosionljud
//Allt ljud stoppas
//5sec

//Svart skärm med Score
//Score är tid och killcount

//Killcount +10 i Score per kill

//TimeBonus +1 per sec under 30min


//1800sec = 30min
//900sec

//30minfloat - totalTimefloat = Timescore

//Timer: 15:31:02
//Kills: 20

//SCORE: 581! (räknar upp)
}
