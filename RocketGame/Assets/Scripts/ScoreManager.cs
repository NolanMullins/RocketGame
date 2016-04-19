using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public float scorePerSecond;
    public Text scoreText;
    public Text pauseScore;
    public Text highScoreText;
    private float score;
    private float highScore;
    private bool isAlive;

	// Use this for initialization
	void Start () {
        //load high score
        highScore = 0;
        if (PlayerPrefs.HasKey("HS"))
            highScore = PlayerPrefs.GetFloat("HS");
        isAlive = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (isAlive)
        {
            score += scorePerSecond * Time.deltaTime;
        }
        scoreText.text = Mathf.Round(score) + " m";
        pauseScore.text = Mathf.Round(score) + " m";
        highScoreText.text = "High Score\n" + Mathf.Round(highScore) + " m";
    }

    public void onExit()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HS", highScore);
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
