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

    //planets
    public ObjectPooler[] planets;
    public int[] spawnAtPoints;
    private bool[] used;
    public PlanetGenerator planetGenerator;

    private string distUnit = "km";

    public LeaderBoard leaderBoard;

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

        used = new bool[planets.Length];

        reset();
    }

    // Update is called once per frame
    void Update() {
        if (isAlive)
        {
            score += scorePerSecond * Time.deltaTime;
            for (int a = 0; a < spawnAtPoints.Length; a++)
            {
                if (spawnAtPoints[a] <= score && !used[a])
                {
                    planetGenerator.spawnPlanet(a, planets[a].getPooledObject());
                    used[a] = true;
                }
            }
        }
        scoreText.text = Mathf.Round(score) + " " + distUnit;
        menuScoreTxt.text = Mathf.Round(score) + " " + distUnit;
        highScoreTxt.text = Mathf.Round(highScore) + " " + distUnit;
    }

    

    public void onExit()
    {
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
        for (int a = 0; a < used.Length; a++)
        {
            used[a] = false;
        }
        planetGenerator.clearPlanets();
    }

    public void scoreTextEnabled(bool enabled)
    {
        scoreText.gameObject.SetActive(enabled);
    }
}
