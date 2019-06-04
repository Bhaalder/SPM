using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour{
    //Author: Patrik Ahlgren
    [SerializeField] private Text killCountText;

    [SerializeField] private Text timeScoreText;
    [SerializeField] private Text killScoreText;

    [SerializeField] private Text totalScoreText;

    [SerializeField] private Text congratulationsText;

    private int maxTimerScore = 1800;

    private int timeScore = 0;
    private int killScore = 0;
    private int totalScore = 0;

    public bool IsEndScreen;

    private void Awake() {
        killCountText.gameObject.SetActive(false);
    }

    private void Update() {
        if (IsEndScreen) {
            totalScoreText.text = totalScore.ToString();
            timeScoreText.text = timeScore.ToString();
            killScoreText.text = killScore.ToString();
        }
    }

    public void CountScore(float totalTime, float killCount) {
        IsEndScreen = true;
        GameController.Instance.Player.GetComponent<PlayerInput>().InputIsFrozen = true;
        killCountText.gameObject.SetActive(true);
        StartCoroutine(Counter(totalTime, killCount));
    }

    private IEnumerator Counter(float totalTime, float killCount) {
        int time = Mathf.RoundToInt(totalTime);
        //animation som förstorar timescore ett kort tag och minskar ner
        for(int i = time; i < maxTimerScore; i++) {
            timeScore++;
            totalScore++;
            yield return new WaitForSeconds(0.03f);
        }
        //animation som förstorar killscore ett kort tag och minskar ner
        for(int i = 0; i < killCount; i++) {
            killScore += 10;
            totalScore += 10;
            yield return new WaitForSeconds(0.03f);
        }
        congratulationsText.text = "Congratulations! You saved humanity from slavery!";
        yield return null;
    }

}
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