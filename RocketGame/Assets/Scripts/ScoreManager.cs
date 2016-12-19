using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public float scorePerSecond;
    public Text scoreText;
    public Text menuScoreTxt;
    public Text highScoreTxt;
    private float score;
    private float highScore;
    private bool isAlive;

    private string distUnit = "km";

    public GPGController leaderBoard;

    // Use this for initialization
    void Start() {
        //load high score
        highScore = 0;
        if (PlayerPrefs.HasKey("HS"))
        {
            highScore = PlayerPrefs.GetFloat("HS");
            
            //leaderBoard.setHighScore((int)Mathf.Round(highScore));
        }
        //highScore = 0;
        isAlive = false;

        reset();
    }

    // Update is called once per frame
    void Update() {
        if (isAlive)
        {
            score += scorePerSecond * Time.deltaTime;
        }
        scoreText.text = Mathf.Round(score) + " " + distUnit;
        menuScoreTxt.text = Mathf.Round(score) + " " + distUnit;
        highScoreTxt.text = Mathf.Round(highScore) + " " + distUnit;
    }

    

    public void onExit()
    {
        leaderBoard.addScore((int)Mathf.Round(score));
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HS", highScore);
            //leaderBoard.submitHighScore((int)Mathf.Round(score));
        }
    }

    public void collectPoint(bool should)
    {
        isAlive = should;
    }

    public void addScore(float add)
    {
        score += add;
    }

    public void reset()
    {
        score = 0;
    }

    public void scoreTextEnabled(bool enabled)
    {
        scoreText.gameObject.SetActive(enabled);
    }
}
